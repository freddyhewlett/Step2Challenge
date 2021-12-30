﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Products;

namespace WebUI.Models.Suppliers
{
    public partial class SupplierViewModel : EntityViewModel
    {        
        public bool Active { get; set; }
        public string FantasyName { get; set; }
        public AddressViewModel Address { get; set; }
        public Guid AddressId { get; set; }
        public EmailViewModel Email { get; set; }
        public Guid EmailId { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public ICollection<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();

        internal void CreatePhoneNumbers(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Phones.Add(new PhoneViewModel());
            }
        }
    }
}
