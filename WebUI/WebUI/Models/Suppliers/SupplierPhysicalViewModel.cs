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
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} deve ter 11 numeros")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime BirthDate { get; set; }
    }
}
