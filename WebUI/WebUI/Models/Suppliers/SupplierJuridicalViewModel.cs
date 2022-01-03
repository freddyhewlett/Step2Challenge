﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class SupplierJuridicalViewModel : SupplierViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Cnpj { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
