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
        public Contacts CheckId([FromRoute] int ID)
        {
            List<Contacts> ListContacts = new List<Contacts>();

            string select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId where Contact.Id=" + ID;

            SqlDataReader reader = AccessDB.AccessReader(select);

            ListContacts = AccessDB.GetContacts(reader);

            return ListContacts[0];
        }

        [HttpGet]
        public List<Contacts> ShowContacts([FromQuery] string filter)
        {
            List<Contacts> ListContacts = new List<Contacts>();

            string select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId";

            if (filter != null)
            {
                select = select + " where Contact.Name LIKE '%" + filter + "%'";
            }

            SqlDataReader reader = AccessDB.AccessReader(select);

            ListContacts = AccessDB.GetContacts(reader);

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
