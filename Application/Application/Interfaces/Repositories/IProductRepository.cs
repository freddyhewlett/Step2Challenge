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
    public interface IProductRepository
    {
        Task<Product> Find(Expression<Func<Product, bool>> predicate);
        Task Insert(Product product);
        Task Remove(Product product);
        Task<IEnumerable<Product>> ToList();
        Task Update(Product product);
        IQueryable<Product> SearchString(string search, Guid? selectedCategory);
        Task<List<Product>> SortProductFilter(string sortOrder);
        Task<IEnumerable<Category>> ListCategories();
        Task<string> FindImagePath(Guid id);
        Task<int> SaveChanges();
        Task<Product> FindById(Guid id);
        Task InsertImage(Image image);
        Task RemoveImage(Image image);
        Task UpdateImage(Image image);
        Task<IEnumerable<SupplierJuridical>> ListJuridicalSuppliers();
        Task<IEnumerable<SupplierPhysical>> ListPhysicalSuppliers();
        Task<IEnumerable<Supplier>> ListAllSuppliersIdFantasy();
    }
}
