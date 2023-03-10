using Address_Book.REST.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using Microsoft.OpenApi.Extensions;
using System.ComponentModel.Design;

namespace Address_Book.REST.Controllers
{
    [ApiController] // Atributes
    [Route("[controller]")] // Atributes
    public class ContactsController : ControllerBase
    {
        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=AddressBook;User Id=sa;Password=S4root;";
        Access_DB AccessDB = new Access_DB();

        [HttpPost]

        public void ReceiveContact(Contacts NewContact)
        {
            AddNewContactDB(NewContact);
            AddPhonesDB(NewContact.Phones);
        }
        public void AddNewContactDB(Contacts NewContact)
        {
            string insert = "insert into Contact (Name, Address, Email) values('" + NewContact.Name + "','" + NewContact.Address + "','" + NewContact.Email.EmailAddress + "')";
            AccessDB.AccessNonQuery(insert);
        }
        public void AddPhonesDB(List<Phones> ListPhones)
        {
            string selectlastId = "select MAX(Id) as Last from Contact";
            SqlDataReader reader = AccessDB.AccessReader(selectlastId);

            int LastID = 0;
            while (reader.Read())
            {
                LastID = Convert.ToInt32(reader["Last"]);
            }
            reader.Close();

            for (int Position = 0; Position < ListPhones.Count; Position++)
            {
                string insertPhone = "insert into Phones (ContactId, Kind, Phone) values(" + LastID + ",'" + ListPhones[Position].Type + "','" + ListPhones[Position].Number + "')";

                AccessDB.AccessNonQuery(insertPhone);
            }
        }

        [HttpGet("{ID}")]
        public Contacts GetContact([FromRoute] int ID)
        {
            string select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId where Contact.Id=" + ID;

            SqlDataReader reader = AccessDB.AccessReader(select);

            int IdLast = 0;
            Contacts CheckContact = new Contacts();
            Email CheckEmail = new Email();
            CheckContact.Phones = new List<Phones>();

            while (reader.Read())
            {
                Phones Phones = new Phones();

                if (IdLast != ID)
                {
                    CheckContact.Id = Convert.ToInt32(reader["Id"]);
                    CheckContact.Name = Convert.ToString(reader["Name"]);
                    CheckContact.Address = Convert.ToString(reader["Address"]);
                    CheckEmail.EmailAddress = Convert.ToString(reader["Email"]);
                    CheckContact.Email = CheckEmail;

                    string CheckPhone = Convert.ToString(reader["Phone"]);

                    if (CheckPhone != "")
                    {
                        Phones.Type = Convert.ToInt32(reader["Kind"]);
                        Phones.Number = CheckPhone;
                        CheckContact.Phones.Add(Phones);
                    }
                }
                else
                {
                    Phones.Type = Convert.ToInt32(reader["Kind"]);
                    Phones.Number = Convert.ToString(reader["Phone"]);

                    CheckContact.Phones.Add(Phones);
                }
                IdLast = ID;
            }
            return CheckContact;
        }

        [HttpGet]
        public List<Contacts> ShowContacts([FromQuery] string filter)
        {
            List<Contacts> ListContacts = new List<Contacts>();

            string select = "";
            if (filter == null)
            {
                select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId";
            }
            else
            {
                select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId where Contact.Name LIKE '%" + filter + "%'";
            }

            SqlDataReader reader = AccessDB.AccessReader(select);

            int IdComparation = 0;

            while (reader.Read())
            {
                int Id = Convert.ToInt32(reader["Id"]);
                string CheckPhone = Convert.ToString(reader["Phone"]);
                Phones AddPhones = new Phones();
                Contacts AddContactList = new Contacts();
                AddContactList.Phones = new List<Phones>();
                Email AddEmail = new Email();

                if (Id != IdComparation)
                {
                    AddContactList.Id = Id;
                    AddContactList.Name = Convert.ToString(reader["Name"]);
                    AddContactList.Address = Convert.ToString(reader["Address"]);

                    AddEmail.EmailAddress = Convert.ToString(reader["Email"]);
                    AddContactList.Email = AddEmail;

                    if (CheckPhone != "")
                    {
                        AddPhones.Type = Convert.ToInt32(reader["Kind"]);
                        AddPhones.Number = Convert.ToString(reader["Phone"]);

                        AddContactList.Phones.Add(AddPhones);
                    }

                    ListContacts.Add(AddContactList);
                }
                else
                {
                    AddPhones.Type = Convert.ToInt32(reader["Kind"]);
                    AddPhones.Number = Convert.ToString(reader["Phone"]);

                    ListContacts[ListContacts.Count - 1].Phones.Add(AddPhones);
                }

                IdComparation = Id;
            }
            return ListContacts;
        }

        [HttpDelete]
        public void RemoveContacts(int NumberID)
        {
            string DeletePhones = "delete from Phones where ContactId=" + NumberID;

            AccessDB.AccessNonQuery(DeletePhones);

            string DeleteContact = "delete from Contact where Id=" + NumberID;

            AccessDB.AccessNonQuery(DeleteContact);

        }

        [HttpPut]
        public void UpdateContacts(Contacts UpdateContact)
        {
            string Update = "update Contact set Name='" + UpdateContact.Name + "', Address='" + UpdateContact.Address + "', Email='" + UpdateContact.Email.EmailAddress + "'where Id=" + UpdateContact.Id;

            AccessDB.AccessNonQuery(Update);

            string Delete = "delete from Phones where ContactId=" + UpdateContact.Id;

            AccessDB.AccessNonQuery(Delete);

            for (int Position = 0; Position < UpdateContact.Phones.Count; Position++)
            {
                string Insert = "insert into Phones (ContactId, Kind, Phone) values(" + UpdateContact.Id + ",'" + UpdateContact.Phones[Position].Type + "','" + UpdateContact.Phones[Position].Number + "')";
                AccessDB.AccessNonQuery(Insert);
            }
        }
    }
}
