using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class Phone
    {
        public Guid PhoneId { get; set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Phone(string ddd, string number)
        {
            Ddd = ddd;
            Number = number;
        }
    }
}
