using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Image : Entity
    {        
        public string ImagePath { get; private set; }  
        public Product Product { get; private set; }
        public Guid ProductId { get; private set; }

        protected Image() { }

        public Image(Guid productId, string path)
        {
            ProductId = productId;
            ImagePath = path;
        }

        public void SetImagePath(string imagePath)
        {
            ImagePath = imagePath;
        }
    }
}
