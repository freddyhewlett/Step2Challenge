using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public abstract class Supplier : Entity
    {


        public bool Active { get; set; }
        public string FantasyName { get; set; }
        public Address Address { get; set; }
        public Guid AddressId { get; set; }
        public Email Email { get; set; }
        public Guid EmailId { get; set; }
        public ICollection<Phone> Phone { get; set; } = new List<Phone>();

        
    }
}
