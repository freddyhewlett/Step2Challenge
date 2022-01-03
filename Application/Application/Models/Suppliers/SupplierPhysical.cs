using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class SupplierPhysical : Supplier
    {
        public string FullName { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        protected SupplierPhysical() { }

        public SupplierPhysical(Guid emailId, Guid addressId, string fantasyName, DateTime birth, string cpf, string fullName, Phone phone)
        {
            EmailId = emailId;
            AddressId = addressId;
            SetBirthDate(birth);
            SetCpf(cpf);
            SetFullName(fullName);
            SetFantasyName(fantasyName);
            SetAddPhone(phone);
        }

        public void SetFullName(string fullName)
        {
            if (FullName != fullName)
            {
                FullName = fullName;
            }            
        }

        public void SetCpf(string cpf)
        {
            if (Cpf != cpf)
            {
                Cpf = cpf;
            }            
        }

        public void SetBirthDate(DateTime birth)
        {
            if (BirthDate != birth)
            {
                BirthDate = birth;
            }
        }
    }
}
