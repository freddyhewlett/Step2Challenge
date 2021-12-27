using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class Email
    {
        public Guid EmailId { get; set; }
        public string EmailAddress { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
