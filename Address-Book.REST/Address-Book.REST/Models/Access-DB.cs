using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.PortableExecutable;

namespace Address_Book.REST.Models
{
    public class Access_DB
    {
        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=AddressBook;User Id=sa;Password=S4root;";
        public void AccessNonQuery(string action)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(action, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
        public SqlDataReader AccessReader(string action)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);

            SqlCommand Command = new SqlCommand(action, Connection);
            Connection.Open();

            SqlDataReader Reader = Command.ExecuteReader();
            return Reader;
        }

        public List<Contacts> GetContacts(SqlDataReader Reader)
        {
            List<Contacts> ListContacts = new List<Contacts>();
            int IdComparation = 0;

            while (Reader.Read())
            {
                int Id = Convert.ToInt32(Reader["Id"]);
                string CheckPhone = Convert.ToString(Reader["Phone"]);
                Phones AddPhones = new Phones();
                Contacts AddContactList = new Contacts();
                AddContactList.Phones = new List<Phones>();
                Email AddEmail = new Email();

                if (Id != IdComparation)
                {
                    AddContactList.Id = Id;
                    AddContactList.Name = Convert.ToString(Reader["Name"]);
                    AddContactList.Address = Convert.ToString(Reader["Address"]);

                    AddEmail.EmailAddress = Convert.ToString(Reader["Email"]);
                    AddContactList.Email = AddEmail;

                    if (CheckPhone != "")
                    {
                        AddPhones.Type = Convert.ToInt32(Reader["Kind"]);
                        AddPhones.Number = Convert.ToString(Reader["Phone"]);

                        AddContactList.Phones.Add(AddPhones);
                    }

                    ListContacts.Add(AddContactList);
                }
                else
                {
                    AddPhones.Type = Convert.ToInt32(Reader["Kind"]);
                    AddPhones.Number = Convert.ToString(Reader["Phone"]);

                    ListContacts[ListContacts.Count - 1].Phones.Add(AddPhones);
                }

                IdComparation = Id;
            }
            return ListContacts;
        }
    }
}
