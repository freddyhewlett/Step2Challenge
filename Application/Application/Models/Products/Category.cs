using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Category : Entity
    {
        
        public bool Active { get; set; }
        public string Name { get; set; }       

        public IEnumerable<Product> Products { get; set; }

        protected Category() { }

        public Category(string name)
        {
            Name = name;
            Active = true;
        }
    }
}
