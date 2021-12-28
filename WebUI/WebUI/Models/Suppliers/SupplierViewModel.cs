using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Products;

namespace WebUI.Models.Suppliers
{
    public abstract class SupplierViewModel
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool Active { get; set; }
        public string FantasyName { get; set; }
        public AddressViewModel Address { get; set; }
        public Guid AddressId { get; set; }
        public EmailViewModel Email { get; set; }
        public Guid EmailId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public ICollection<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
    }
}
