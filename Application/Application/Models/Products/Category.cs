using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Category : Entity
    {
        
        public bool Active { get; private set; }
        public string Name { get; private set; }       

        public IEnumerable<Product> Products { get; private set; }

        protected Category() { }

        public Category(string name)
        {
            Name = name;
            Active = true;
        }

        public void SetCategoryName(string name)
        {
            Name = name;
        }
    }
}
