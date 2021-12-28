using Domain.Models.Enum;
using Domain.Models.Products;
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
        public IEnumerable<Product> Products { get; set; }
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();

    }
}
