using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class SupplierJuridicalViewModel : SupplierViewModel
    {
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 e 255 caracteres")]
        [Display(Name = "Razão Social")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "{0} deve ter 14 numeros")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Required]
        [Display(Name = "Data de fundação da empresa")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime OpenDate { get; set; }
    }
}
