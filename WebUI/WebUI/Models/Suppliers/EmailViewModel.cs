using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class EmailViewModel
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public string EmailAddress { get; set; }
    }
}
