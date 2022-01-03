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
        [Required]
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();

        [Required]
        public string BarCode { get; set; }

        [Required]
        public int QuantityStock { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public decimal PriceSales { get; set; }

        [Required]
        public decimal PricePurchase { get; set; }
        public SupplierViewModel Supplier { get; set; }
        public Guid SupplierId { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
