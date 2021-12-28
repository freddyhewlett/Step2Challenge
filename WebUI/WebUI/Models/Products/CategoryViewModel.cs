﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Products
{
    public class CategoryViewModel
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool Active { get; set; }
        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
