using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class SupplierJuridical : Supplier
    {

        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public DateTime OpenDate { get; set; }


        protected SupplierJuridical() { }

        public SupplierJuridical(Guid emailId, Guid addressId, string fantasyName, DateTime open, string cnpj, string companyName, string ddd, string number, PhoneType type)
        {
            EmailId = emailId;
            AddressId = addressId;
            FantasyName = fantasyName;
            OpenDate = open;
            Cnpj = cnpj;
            CompanyName = companyName;

            SetPhone(ddd, number, type);
        }

        public void SetPhone(string ddd, string number, PhoneType type)
        {

            if (Phones.Count >= 3)
            {
                throw new Exception("Quantidade limite de numeros telefonicos atingido");
            }

            Phones.Add(new Phone(ddd, number, type));

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
