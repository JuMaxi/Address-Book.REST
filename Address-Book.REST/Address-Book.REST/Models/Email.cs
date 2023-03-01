using System;

namespace Address_Book.REST.Models
{
    public class Email
    {
        public string EmailAddress { get; set; }

        public void ValidateEmail(string Email)
        {
            if (Email.Length > 0)
            {
                bool ValideEmail = false;

                if (Email.IndexOf("@") > 0)
                {
                    int Check = Email.IndexOf("@");

                    for (int Position = Check + 2; Position < Email.Length; Position++)
                    {
                        if (Position < Email.Length)
                        {
                            if (Email[Position] == 46)
                            {
                                if (Position != (Email.Length - 1))
                                {
                                    ValideEmail = true;
                                }
                            }
                        }
                    }
                }
                if ((Email.IndexOf(" ") > 0)
                || (ValideEmail == false))
                {
                    throw new Exception("This email is invalid. Put a valid email to continue this registry.");
                }
            }
        }
    }
}
