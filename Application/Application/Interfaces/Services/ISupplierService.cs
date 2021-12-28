using Domain.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<SupplierPhysical> FindPhysicalById(Guid id);
        Task<SupplierJuridical> FindJuridicalById(Guid id);
        Task<IEnumerable<SupplierPhysical>> PhysicalAll();
        Task<IEnumerable<SupplierJuridical>> JuridicalAll();
        Task InsertPhysical(SupplierPhysical supplier);
        Task InsertJuridical(SupplierJuridical supplier);
        Task RemovePhysical(Guid id);
        Task RemoveJuridical(Guid id);
        Task UpdatePhysical(SupplierPhysical supplier);
        Task UpdateJuridical(SupplierJuridical supplier);
        IQueryable<SupplierPhysical> SearchPhysical(string search, Guid? selectedGenre);
        IQueryable<SupplierJuridical> SearchJuridical(string search, Guid? selectedGenre);
        Task InsertPhone(Phone phone);
        Task RemovePhone(Phone phone);
        Task UpdatePhone(Phone phone);
        Task UpdateAddress(Address address);
        Task UpdateEmail(Email email);
    }
}
