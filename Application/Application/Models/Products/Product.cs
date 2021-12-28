using Domain.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Product : Entity
    {        
        public string Name { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Image> Image { get; set ; } = new List<Image>();
        public string BarCode { get; set; }
        public int QuantityStock { get; set; }
        public bool Active { get; set; }
        public decimal PriceSales { get; set; }
        public decimal PricePurchase { get; set; }
        public Supplier Supplier { get; set; }
        public Guid SupplierId { get; set; }

        protected Product() 
        {            
        }

        public Product(Guid idCategory, List<Image> images, Guid idSupplier, string name, string barCode, int qtyStock, bool active, decimal sale, decimal purchase)
        {
            CategoryId = idCategory;

            foreach (var item in images)
            {
                SetImagePath(item.ImagePath);
            }
            if (Image.Count < 1) throw new Exception("Imagem obrigatória");

            Name = name;
            BarCode = barCode;
            QuantityStock = qtyStock;
            Active = active;
            PriceSales = sale;
            PricePurchase = purchase;
            SupplierId = idSupplier;
        }

        public void SetImagePath(string imagePath)
        {            

            if (Image.Count >= 5)
            {
                //TODO retornar erro
            }

            Image.Add(new Image(imagePath));

        }


    }
}
