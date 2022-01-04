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
            if (!PhoneExists(phone.PhoneType))
            {
                SetAddPhone(new Phone(Id, phone.Ddd, phone.Number, phone.PhoneType));
            }
            else
            {
                var phoneExist = Phones.Where(x => x.PhoneType == phone.PhoneType).FirstOrDefault();

                if (phoneExist.Number != phone.Number)
                {
                    phoneExist.SetPhone(phone);
                }                
            }
        }

        public void SetRemovePhone(Phone phone)
        {
            if (PhoneExists(phone.PhoneType))
            {
                Phones.Remove(phone);
            }
            else
            {
                throw new Exception("Numero telefonico inexistente não pode ser removido.");
            }            
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
            if (Email == null)
            {
                Email = new Email(email);
            }
            else
            {
                Email.SetEmail(email);
            }                        
        }

        public void SetFantasyName(string name)
        {
            FantasyName = name;
        }

        public void SetAddress(Address address)
        {
            if (Address == null)
            {
                Address = new Address(address.ZipCode, address.Street, address.Number, address.Complement, address.Reference, address.Neighborhood, address.City, address.State);
            }
            else
            {
                Address.SetAddress(address.ZipCode, address.Street, address.Number, address.Complement, address.Reference, address.Neighborhood, address.City, address.State);
            }
        }

        public bool PhoneExists(PhoneType type)
        {
            return Phones.Where(x => x.PhoneType == type).FirstOrDefault() != null;
        }
    }
}
