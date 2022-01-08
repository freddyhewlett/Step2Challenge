using Domain.Models;
using Domain.Models.Products;
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
        Task UpdatePhysical(SupplierPhysical supplier);
        Task UpdateJuridical(SupplierJuridical supplier);
        Task<IEnumerable<SupplierPhysical>> ToListPhysical();
        Task<IEnumerable<SupplierJuridical>> ToListJuridical();
        IQueryable<SupplierPhysical> SearchPhysicalString(string search);
        Task<List<SupplierJuridical>> SortJuridicalFilter(string sortOrder);
        IQueryable<SupplierJuridical> SearchJuridicalString(string search);
        Task<List<SupplierPhysical>> SortPhysicalFilter(string sortOrder);
        Task<SupplierPhysical> FindPhysicalById(Guid id);
        Task<SupplierJuridical> FindJuridicalById(Guid id);
        Task<IEnumerable<Product>> ProductListByPhysical(Guid id);
        Task<IEnumerable<Product>> ProductListByJuridical(Guid id);
        Task InsertPhone(Phone phone);
        Task RemovePhone(Phone phone);
        Task UpdatePhone(Phone phone);
        Task UpdateAddress(Address address);
        Task UpdateEmail(Email email);
        Task<int> SaveChanges();
        Task InsertAddress(Address address);
        Task InsertEmail(Email email);
        Task<PaginationViewModel<SupplierPhysical>> PaginationPhysical(int pageSize, int pageIndex, string query);
        Task<PaginationViewModel<SupplierJuridical>> PaginationJuridical(int pageSize, int pageIndex, string query);
    }
}
