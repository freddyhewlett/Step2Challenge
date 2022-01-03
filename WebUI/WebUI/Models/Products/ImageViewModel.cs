using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Products
{
    public class ImageViewModel : EntityViewModel
    {
        [Required]
        public string ImagePath { get; set; }
    }
}
