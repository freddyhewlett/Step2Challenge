using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
