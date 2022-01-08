using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INotifierService _notifierService;

        public CategoryService(ICategoryRepository categoryRepository, INotifierService notifierService)
        {
            _categoryRepository = categoryRepository;
            _notifierService = notifierService;
        }

        public async Task<Category> FindById(Guid id)
        {
            var result = await _categoryRepository.FindById(id);

            if (result == null)
            {
                _notifierService.AddError("Produto inexistente.");
            }

            return result;
        }

        public async Task<IEnumerable<Category>> CategoryAll()
        {
            return await _categoryRepository.ToList();
        }

        public async Task Insert(Category category)
        {
            if (_categoryRepository.Find(x => x.Name == category.Name).Result != null)
            {
                _notifierService.AddError("Categoria já cadastrada. \nFavor alterar o nome da categoria.");
                return;
            }

            await _categoryRepository.Insert(category);
            await _categoryRepository.SaveChanges();
        }

        public async Task Remove(Guid id)
        {
            var remove = await FindById(id);
            if (_notifierService.HasError()) return;

            await _categoryRepository.Remove(remove);
            await _categoryRepository.SaveChanges();
        }

        public async Task Update(Category category)
        {
            //TODO Fluent Validation (classe) para categoria
            if (_notifierService.HasError()) return;

            var result = await _categoryRepository.FindById(category.Id);

            if (result == null)
            {
                _notifierService.AddError("Produto não encontrado");
            }

            result.SetCategoryName(category.Name);


            await _categoryRepository.Update(result);
            await _categoryRepository.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> ListProducts()
        {
            return await _categoryRepository.ListProducts();
        }

        public IQueryable<Category> SearchString(string search)
        {
            var products = _categoryRepository.SearchString(search);

            return products;
        }

        public async Task<List<Category>> SortFilter(string sortOrder)
        {
            var sort = await _categoryRepository.SortCategoryFilter(sortOrder);
            return sort;
        }

        public async Task<PaginationViewModel<Category>> Pagination(int pageSize, int pageIndex, string query)
        {
            return await _categoryRepository.Pagination(pageSize, pageIndex, query);
        }
    }
}
