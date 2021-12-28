using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class Phone : Entity
    {
        public string Ddd { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }

        public Phone(string ddd, string number, PhoneType phoneType)
        {
            Ddd = ddd;
            Number = number;
            PhoneType = phoneType;
        }
    }
}
