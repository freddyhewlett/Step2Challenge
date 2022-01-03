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

        public SupplierJuridical(Guid emailId, Guid addressId, string fantasyName, DateTime open, string cnpj, string companyName, Phone phone)
        {
            EmailId = emailId;
            AddressId = addressId;
            SetOpenDate(open);
            SetCnpj(cnpj);
            SetCompanyName(companyName);
            SetFantasyName(fantasyName);
            SetAddPhone(phone);
        }

        public void SetCompanyName(string companyName)
        {
            CompanyName = companyName;
        }

        public void SetCnpj(string cnpj)
        {
            Cnpj = cnpj;
        }

        public void SetOpenDate(DateTime open)
        {
            OpenDate = open;
        }

    }
}
