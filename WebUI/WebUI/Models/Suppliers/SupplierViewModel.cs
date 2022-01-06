using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Enum;
using WebUI.Models.Products;
using WebUI.Utilities;

namespace WebUI.Models.Suppliers
{
    public partial class SupplierViewModel : EntityViewModel
    {
        public bool Active { get; set; }

        [Required(ErrorMessage ="O {0} deve ser preenchido")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} deve ter entre 5 e 255 caracteres")]
        [Display(Name ="Nome Fantasia")]
        public string FantasyName { get; set; }
        public AddressViewModel Address { get; set; }
        public Guid AddressId { get; set; }
        public EmailViewModel Email { get; set; }
        public Guid EmailId { get; set; }
        
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} deve ter 11 numeros (incluindo DDD)")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "Telefone Celular")]
        public string MobilePhone { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "Telefone Residencial")]
        public string HomePhone { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} deve conter apenas numeros")]
        [Display(Name = "Telefone Empresarial")]
        public string OfficePhone { get; set; }
        public PhoneType PhoneType { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public ICollection<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
    }
}
