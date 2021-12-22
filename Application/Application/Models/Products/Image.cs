using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Products
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public void SetImagePath(string imagePath)
        {
            
        }
    }
}
