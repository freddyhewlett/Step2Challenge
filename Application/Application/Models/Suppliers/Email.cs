using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class Email : Entity
    {
        public string EmailAddress { get; private set; }

        public Email() { }

        public Email(string email)
        {
            EmailAddress = email;
        }

        public void SetEmail(string email)
        {
            EmailAddress = email;
        }
    }
}
