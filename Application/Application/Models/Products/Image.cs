using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Image : Entity
    {        
        public string ImagePath { get; set; }

        protected Image() { }

        public Image(string path)
        {
            ImagePath = path;
        }
    }
}
