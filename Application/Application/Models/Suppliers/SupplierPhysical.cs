using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class SupplierPhysical : Supplier
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
