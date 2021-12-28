using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Products
{
    public class ImageViewModel
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public string ImagePath { get; set; }
    }
}
