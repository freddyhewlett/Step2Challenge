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

        [Required(ErrorMessage = "Campo {0} deve ser preenchido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ter minimo 3 e maximo 60 caracteres")]
        [Display(Name = "Nome da Categoria")]
        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public CategoryViewModel() { }

        public CategoryViewModel(string name, IEnumerable<ProductViewModel> products)
        {
            Name = name;
            Active = true;
            Products = products;
        }
    }
}
