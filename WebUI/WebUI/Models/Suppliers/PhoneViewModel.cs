using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class PhoneViewModel
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
        public Domain.Models.Enum.PhoneType PhoneType { get; set; }
    }
}
