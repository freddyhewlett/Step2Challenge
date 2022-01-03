using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class SupplierPhysicalViewModel : SupplierViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
