using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Suppliers
{
    public class EmailViewModel : EntityViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "Endereço de E-mail")]
        public string EmailAddress { get; set; }
    }
}
