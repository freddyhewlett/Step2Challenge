using Domain.Models;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Find(Expression<Func<Category, bool>> predicate);
        Task<IEnumerable<Product>> FindAll(Expression<Func<Product, bool>> predicate);
        Task<IEnumerable<Product>> ListCategories();
        Task Insert(Category category);
        Task Remove(Category category);
        Task<IEnumerable<Category>> ToList();
        Task Update(Category category);
        Task<int> SaveChanges();
        Task<Category> FindById(Guid id);
        Task<IEnumerable<Product>> ListProducts();
        IQueryable<Category> SearchString(string search);
        Task<List<Category>> SortCategoryFilter(string sortOrder);
        Task<PaginationViewModel<Category>> Pagination(int pageSize, int pageIndex, string query);
    }
}
