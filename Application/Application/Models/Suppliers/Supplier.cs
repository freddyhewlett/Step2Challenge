using Domain.Models.Enum;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Models.Suppliers
{
    public abstract class Supplier : Entity
    {


        public bool Active { get; private set; }
        public string FantasyName { get; private set; }
        public Address Address { get; private set; }
        public Guid AddressId { get; protected set; }
        public Email Email { get; private set; }
        public Guid EmailId { get; protected set; }
        public IEnumerable<Product> Products { get; private set; }
        public ICollection<Phone> Phones { get; private set; } = new List<Phone>();

        public void SetUpdatePhone(Phone phone) 
        {
            var result = Phones.Where(x => x.PhoneType == phone.PhoneType).FirstOrDefault();

            if (result == null)
            {
                SetAddPhone(new Phone(phone.Ddd, phone.Number, phone.PhoneType));
            }
            else
            {
                SetRemovePhone(phone);
                result.SetPhone(phone);
            }
        }

        public void SetRemovePhone(Phone phone)
        {
            Phones.Remove(phone);
        }

        public void SetAddPhone(Phone phone)
        {
            if (Phones.Count >= 3)
            {
                throw new Exception("Quantidade limite de numeros telefonicos atingido");
            }

            Phones.Add(phone);
        }

        public void SetEmail(string email)
        {
            if (Email.EmailAddress != email)
            {
                Email = new Email(email);
            }            
        }

        public void SetFantasyName(string name)
        {
            FantasyName = name;
        }

        public void SetAddress(Address address)
        {
            if (Address.ZipCode != address.ZipCode)
            {
                Address = new Address(address.ZipCode, address.Street, address.Number, address.Complement, address.Reference, address.Neighborhood, address.City, address.State);
            }
        }
    }
}
