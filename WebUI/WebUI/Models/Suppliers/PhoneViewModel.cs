using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Enum;

namespace WebUI.Models.Suppliers
{
    public class PhoneViewModel : EntityViewModel
    {
        public string Ddd { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public Guid SupplierId { get; set; }
    }
}
