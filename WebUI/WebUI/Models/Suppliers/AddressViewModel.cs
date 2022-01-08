using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class AddressViewModel : EntityViewModel
    {
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "{0} deve ter 8 numeros")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "CEP")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 e 255 caracteres")]
        [Display(Name = "Logradouro")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(6, MinimumLength = 2, ErrorMessage = "{0} deve ter 8 numeros")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "Nº")]
        public string Number { get; set; }

        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Display(Name = "Referência")]
        public string Reference { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 e 255 caracteres")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 150 caracteres")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "{0} deve ter entre 2 e 60 caracteres")]
        [Display(Name = "Estado/UF")]
        public string State { get; set; }
    }
}
