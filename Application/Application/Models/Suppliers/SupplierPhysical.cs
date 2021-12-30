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

        public SupplierPhysical(Guid emailId, Guid addressId, string fantasyName, DateTime birth, string cpf, string fullName, Phone phone)
        {
            EmailId = emailId;
            AddressId = addressId;
            FantasyName = fantasyName;
            BirthDate = birth;
            Cpf = cpf;
            FullName = fullName;

            SetPhone(phone);
        }

        public void SetPhone(Phone phone)
        {

            if (Phones.Count >= 3)
            {
                throw new Exception("Quantidade limite de numeros telefonicos atingido");
            }

            Phones.Add(phone);

        }

        public void CreatePhoneNumbers(int count = 1)
        {
            if (count >= 3)
            {
                throw new Exception("Quantidade limite de numeros telefonicos atingido");
            }

            for (int i = 0; i < count; i++)
            {
                Phones.Add(new Phone());
            }
        }
    }
}
