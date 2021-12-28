using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class SupplierJuridicalViewModel : SupplierViewModel
    {
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
