using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.Suppliers;
using WebUI.Models.Products;

namespace WebUI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SupplierViewModel, Domain.Models.Suppliers.Supplier>().ReverseMap();
            CreateMap<SupplierPhysicalViewModel, Domain.Models.Suppliers.SupplierPhysical>().ReverseMap();
            CreateMap<SupplierJuridicalViewModel, Domain.Models.Suppliers.SupplierJuridical>().ReverseMap();
            CreateMap<PhoneViewModel, Domain.Models.Suppliers.Phone>().ReverseMap();
            CreateMap<EmailViewModel, Domain.Models.Suppliers.Email>().ReverseMap();
            CreateMap<AddressViewModel, Domain.Models.Suppliers.Address>().ReverseMap();
            CreateMap<CategoryViewModel, Domain.Models.Products.Category>().ReverseMap();
            CreateMap<ImageViewModel, Domain.Models.Products.Image>().ReverseMap();
            CreateMap<ProductViewModel, Domain.Models.Products.Product>().ReverseMap();
        }
    }
}
