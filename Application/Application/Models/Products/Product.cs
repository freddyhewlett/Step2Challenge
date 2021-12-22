using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Product : Entity
    {
        private Image[] image;
        private Guid[] imageId;

        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public Image[] Image { get => image; set => image = new Image[5]; }
        public Guid[] ImageId { get => imageId; set => imageId = new Guid[5]; }
        public string BarCode { get; set; }
        public int QuantityStock { get; set; }
        public bool Active { get; set; }
        public decimal PriceSales { get; set; }
        public decimal PricePurchase { get; set; }



        public Product() { }

        public Product(Guid idCategory, Guid idImage, string name, string barCode, int qtyStock, bool active, decimal sale, decimal purchase)
        {
            CategoryId = idCategory;
            ImageId[0] = idImage;
            Name = name;
            BarCode = barCode;
            QuantityStock = qtyStock;
            Active = active;
            PriceSales = sale;
            PricePurchase = purchase;
            Image[0].InsertDate = DateTime.Now;
        }

        public void SetImagePath(int imageIndex, string imagePath, Guid idImage)
        {
            Image[imageIndex].ImagePath = imagePath;
            Image[imageIndex].UpdateDate = DateTime.Now;
            ImageId[imageIndex] = idImage;
        }
    }
}
