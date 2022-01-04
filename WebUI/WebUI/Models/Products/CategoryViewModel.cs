using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Products
{
    public class CategoryViewModel : EntityViewModel
    {
        [Required]
        public bool Active { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public CategoryViewModel(string name, IEnumerable<ProductViewModel> products)
        {
            Name = name;
            Active = true;
            Products = products;
        }
    }
}
