using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Suppliers
{
    public class SupplierJuridical : Supplier
    {
        public string CompanyName { get; set; }        
        public string Cnpj { get; set; }
        public DateTime OpenDate { get; set; }





        public void SetPhone(string ddd, string number)
        {

            if (Phone.Count >= 3)
            {
                //TODO retornar erro
            }

            Phone.Add(new Phone(ddd, number));

        }
    }
}
