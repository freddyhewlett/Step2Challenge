using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class SupplierPhysicalViewModel : SupplierViewModel
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
