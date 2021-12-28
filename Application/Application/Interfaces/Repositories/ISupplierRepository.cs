using Domain.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task<SupplierPhysical> FindPhysical(Expression<Func<SupplierPhysical, bool>> predicate);
        Task<SupplierJuridical> FindJuridical(Expression<Func<SupplierJuridical, bool>> predicate);
        Task InsertPhysical(SupplierPhysical supplier);
        Task InsertJuridical(SupplierJuridical supplier);
        Task RemovePhysical(SupplierPhysical supplier);
        Task RemoveJuridical(SupplierJuridical supplier);
        Task<IEnumerable<SupplierPhysical>> ToListPhysical();
        Task<IEnumerable<SupplierJuridical>> ToListJuridical();
        IQueryable<SupplierPhysical> SearchPhysicalString(string search, Guid? id);
        IQueryable<SupplierJuridical> SearchJuridicalString(string search, Guid? id);
        Task<int> SaveChanges();
        Task<SupplierPhysical> FindPhysicalById(Guid id);
        Task<SupplierJuridical> FindJuridicalById(Guid id);
    }
}
