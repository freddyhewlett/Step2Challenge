using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class Phone : Entity
    {        
        public string Ddd { get; private set; }
        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }
        public Supplier Supplier { get; private set; }
        public Guid SupplierId { get; private set; }

        public Phone() { }

        public Phone(string ddd, string number, PhoneType phoneType)
        {
            Ddd = ddd;
            Number = number;
            PhoneType = phoneType;
        }

        public void SetPhone (Phone phone)
        {
            Ddd = phone.Ddd;
            Number = phone.Number;
            PhoneType = phone.PhoneType;
        }
    }
}
