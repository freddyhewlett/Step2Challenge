using Application.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly INotifierService _notifierService;

        public ProductService(IProductRepository productRepository, INotifierService notifierService)
        {
            _productRepository = productRepository;
            _notifierService = notifierService;
        }

        public async Task<Product> FindById(Guid id)
        {
            var result = await _productRepository.FindById(id);

            if (result == null)
            {
                _notifierService.AddError("Produto inexistente.");
            }

            return result;
        }

        public async Task<IEnumerable<Product>> ProductsAll()
        {
            return await _productRepository.ToList();
        }

        public async Task Insert(Product product)
        {
            if (_productRepository.Find(x => x.Name == product.Name).Result != null)
            {
                _notifierService.AddError("Produto já cadastrado. \nFavor alterar o nome do produto.");
                return;
            }

            await _productRepository.Insert(product);
            await _productRepository.SaveChanges();
        }

        public async Task Remove(Guid id)
        {
            var remove = await FindById(id);
            if (_notifierService.HasError()) return;

            await _productRepository.Remove(remove);
            await _productRepository.SaveChanges();
        }

        public async Task Update(Product product)
        {

            if (_notifierService.HasError()) return;

            await _productRepository.Update(product);
            await _productRepository.SaveChanges();
        }

        public async Task<IEnumerable<Category>> ListCategories()
        {
            return await _productRepository.ListCategories();
        }

        public async Task<List<Product>> SortFilter(string sortOrder)
        {
            var sort = await _productRepository.SortProductFilter(sortOrder);
            return sort;
        }

        public IQueryable<Product> SearchString(string search, Guid? selectedCategory)
        {
            var products = _productRepository.SearchString(search, selectedCategory);

            return products;
        }

        public async Task<string> FindImagePath(Guid id)
        {
            return await _productRepository.FindImagePath(id);
        }

        //public async Task InsertImage()
        //{
        //    //TODO
        //}

        //public async Task UpdateImage()
        //{
        //    //TODO
        //}

        //public async Task RemoveImage()
        //{
        //    //TODO
        //}
    }
}
