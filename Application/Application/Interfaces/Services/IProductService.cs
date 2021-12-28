using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> FindById(Guid id);
        Task<IEnumerable<Product>> ProductsAll();
        Task Insert(Product product);
        Task Remove(Guid id);
        Task Update(Product product);
        Task<IEnumerable<Category>> ListCategories();
        Task<List<Product>> SortFilter(string sortOrder);
        IQueryable<Product> SearchString(string search, Guid? selectedCategory);
        Task<string> FindImagePath(Guid id);
        //Task InsertImage();
        //Task UpdateImage();
        //Task RemoveImage();
    }
}
