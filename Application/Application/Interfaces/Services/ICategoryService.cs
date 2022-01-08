using Domain.Models;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Category> FindById(Guid id);
        Task<IEnumerable<Category>> CategoryAll();
        Task Insert(Category category);
        Task Remove(Guid id);
        Task Update(Category category);
        Task<IEnumerable<Product>> ListProducts();
        Task<List<Category>> SortFilter(string sortOrder);
        IQueryable<Category> SearchString(string search);
        Task<PaginationViewModel<Category>> Pagination(int pageSize, int pageIndex, string query);
    }
}
