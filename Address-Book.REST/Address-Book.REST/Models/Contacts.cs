using System.Collections.Generic;
using System;

namespace Address_Book.REST.Models
{
    public class Contacts
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public Email Email { get; set; }
        public List<Phones> Phones { get; set; }
        public int Id { get; set; }


        public void ValidateContacts()
        {
            string NameTrim = Name.Trim();
            Name = NameTrim;

            if (Name.IndexOf(" ") < 0)
            {
                throw new Exception("In this field you need to put the Name + Last Name.");
            }
            if (Name == " ")
            {
                throw new Exception("This field is Mandatory. You need to put the Name + Last Name for to complet this registration.");
            }
            if ((Address.Length == 0)
                && (Phones.Count == 0))
            {
                Console.WriteLine(" ");
                throw new Exception("You need to put at least one field more to complet this registration");
            }
        }
    }
}

