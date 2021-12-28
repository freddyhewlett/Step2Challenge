using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class SupplierPhysical : Supplier
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }

        protected SupplierPhysical() { }

        public SupplierPhysical(Guid emailId, Guid addressId, string fantasyName, DateTime birth, string cpf, string fullName, string ddd, string number, PhoneType type)
        {
            EmailId = emailId;
            AddressId = addressId;
            FantasyName = fantasyName;
            BirthDate = birth;
            Cpf = cpf;
            FullName = fullName;

            SetPhone(ddd, number, type);
        }

        public void SetPhone(string ddd, string number, PhoneType type)
        {

            if (Phones.Count >= 3)
            {
                //TODO retornar erro
            }

            Phones.Add(new Phone(ddd, number, type));

        }
    }
}
