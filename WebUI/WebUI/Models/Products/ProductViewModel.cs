using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Suppliers;

namespace WebUI.Models.Products
{
    public class ProductViewModel : EntityViewModel
    {
        [Required(ErrorMessage = "O {0} deve ser preenchido")]
        [StringLength(124, MinimumLength = 4, ErrorMessage = "{0} deve ter entre 4 e 124 caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Categoria")]
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }

        [Display(Name = "Fotos")]
        public ICollection<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [Display(Name = "Código de barras")]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "Quantidade")]
        public int QuantityStock { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal PriceSales { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal PricePurchase { get; set; }
        
        [Display(Name = "Fornecedor")]
        public SupplierViewModel Supplier { get; set; }
        public Guid SupplierId { get; set; }

        [Display(Name = "Upload de Imagem")]
        public ICollection<IFormFile> ImagesUpload { get; set; } = new List<IFormFile>();

        [Display(Name = "Categorias")]
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Display(Name = "Fornecedores")]
        public IEnumerable<SupplierViewModel> Suppliers { get; set; }


        public ProductViewModel() { }
    }
}
