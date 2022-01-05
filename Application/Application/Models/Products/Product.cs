using Domain.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Product : Entity
    {        
        public string Name { get; private set; }
        public Category Category { get; private set; }
        public Guid CategoryId { get; private set; }
        public ICollection<Image> Images { get; private set; } = new List<Image>();
        public string BarCode { get; private set; }
        public int QuantityStock { get; private set; }
        public bool Active { get; private set; }
        public decimal PriceSales { get; private set; }
        public decimal PricePurchase { get; private set; }
        public Supplier Supplier { get; private set; }
        public Guid SupplierId { get; private set; }

        protected Product() 
        {            
        }

        public Product(Guid idCategory, List<Image> images, Guid idSupplier, string name, string barCode, int qtyStock, bool active, decimal sale, decimal purchase)
        {
            SupplierId = idSupplier;
            CategoryId = idCategory;

            foreach (var item in images)
            {
                SetImagePath(item.ImagePath);
            }
            if (Images.Count < 1) throw new Exception("Imagem obrigatória");

            SetName(name);
            SetBarCode(barCode);
            SetQuantityStock(qtyStock);
            SetActive(active);
            SetPriceSales(sale);
            SetPricePurchase(purchase);
        }

        public void SetImagePath(string imagePath)
        {            

            if (Images.Count >= 5)
            {
                throw new Exception("Numero maximo de imagens atingido");
            }

            Images.Add(new Image(Id, imagePath));

        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetBarCode(string barCode)
        {
            BarCode = barCode;
        }

        public void SetQuantityStock(int stock)
        {
            QuantityStock = stock;
        }

        public void SetPriceSales(decimal sales)
        {
            PriceSales = sales;
        }

        public void SetPricePurchase(decimal purchase)
        {
            PricePurchase = purchase;
        }

        public void SetActive (bool active)
        {
            Active = active;
        }
    }
}
