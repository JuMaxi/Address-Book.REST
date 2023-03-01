using Address_Book.REST.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using Microsoft.OpenApi.Extensions;

namespace Address_Book.REST.Controllers
{
    [ApiController] // Atributes
    [Route("[controller]")] // Atributes
    public class ContactsController : ControllerBase
    {
        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=AddressBook;User Id=sa;Password=S4root;";

        [HttpPost]

        public void ReceiveContact(Contacts NewContact)
        {
            AddNewContactDB(NewContact);
            AddPhonesDB(NewContact.Phones);
        }
        public void AddNewContactDB(Contacts NewContact)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string insert = "insert into Contact (Name, Address, Email) values('" + NewContact.Name + "','" + NewContact.Address + "','" + NewContact.Email.EmailAddress + "')";
                SqlCommand command = new SqlCommand(insert, connection);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        public void AddPhonesDB(List<Phones> ListPhones)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string selectlastId = "select MAX(Id) as Last from Contact";
                SqlCommand command = new SqlCommand(selectlastId, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                int LastID = 0;
                while (reader.Read())
                {
                    LastID = Convert.ToInt32(reader["Last"]);
                }
                reader.Close();

                for (int Position = 0; Position < ListPhones.Count; Position++)
                {
                    string insertPhone = "insert into Phones (ContactId, Kind, Phone) values(" + LastID + ",'" + ListPhones[Position].Type + "','" + ListPhones[Position].Number + "')";

                    SqlCommand commandinsert = new SqlCommand(insertPhone, connection);
                    commandinsert.ExecuteNonQuery();
                }
            }
        }

        [HttpGet]
        public List<Contacts> ShowContacts()
        {
            List<Contacts> ListContacts = new List<Contacts>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId";

                SqlCommand command = new SqlCommand(select, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

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
            }
            return ListContacts;
        }

    }
}
