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
        public ProductViewModel ProductViewmodel { get; private set; }
        public Guid ProductViewModelId { get; private set; }


        public ImageViewModel() { }

        public ImageViewModel(string path) 
        {
            ImagePath = path;
        }
    }
}
