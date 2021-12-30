using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Suppliers;

namespace WebUI.Models.Products
{
    public class ProductViewModel : EntityViewModel
    {
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();
        public string BarCode { get; set; }
        public int QuantityStock { get; set; }
        public bool Active { get; set; }
        public decimal PriceSales { get; set; }
        public decimal PricePurchase { get; set; }
        public SupplierViewModel Supplier { get; set; }
        public Guid SupplierId { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
