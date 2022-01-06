using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class SupplierPhysicalViewModel : SupplierViewModel
    {
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 e 255 caracteres")]
        [Display(Name ="Nome Completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 e 255 caracteres")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
