using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Products;
using Domain.Models.Suppliers;
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

            if (product.Images.Count < 1 || product.Images.Count > 5)
            {
                _notifierService.AddError("Produto deve ter minimo 1 e maximo 5 imagens");
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
            //TODO Fluent Validation (classe) para imagem
            if (_notifierService.HasError()) return;

            var result = await _productRepository.FindById(product.Id);

            if (result == null)
            {
                _notifierService.AddError("Produto não encontrado");
            }

            result.SetName(product.Name);
            result.SetActive(product.Active);
            result.SetBarCode(product.BarCode);
            result.SetPricePurchase(product.PricePurchase);
            result.SetPriceSales(product.PriceSales);
            result.SetQuantityStock(product.QuantityStock);
            result.SetSupplierId(product.SupplierId);
            result.SetCategoryId(product.CategoryId);
            if (product.Images.Count > 0 && product.Images.Count <= 5)
            {
                foreach (var item in result.Images)
                {
                    if (product.Images.Where(x => x.Id == item.Id).FirstOrDefault() != null)
                        continue;
                }
                foreach (var item in product.Images)
                {
                    if (result.Images.Where(x => x.Id == item.Id).FirstOrDefault() != null)
                        continue;
                    else
                        result.SetImage(item);
                    await _productRepository.InsertImage(item);
                }
            }
            

            await _productRepository.Update(result);
            await _productRepository.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Category>> ListCategories()
        {
            return await _productRepository.ListCategories();
        }

        public async Task<IEnumerable<SupplierPhysical>> ListPhysicalSuppliers()
        {
            return await _productRepository.ListPhysicalSuppliers();
        }

        public async Task<IEnumerable<SupplierJuridical>> ListJuridicalSuppliers()
        {
            return await _productRepository.ListJuridicalSuppliers();
        }

        public async Task<IEnumerable<Supplier>> ListAllSuppliersIdFantasy()
        {
            return await _productRepository.ListAllSuppliersIdFantasy();
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

        public async Task<string> FindImagePathByImageId(Guid id)
        {
            return await _productRepository.FindImagePathByImageId(id);
        }

        public async Task<string> FindImagePathByProductId(Guid id)
        {
            return await _productRepository.FindImagePathByProductId(id);
        }

        public async Task<Product> FindProductByImageId(Guid id)
        {
            return await _productRepository.FindProductByImageId(id);
        }

        public async Task<Image> FindImageById(Guid id)
        {
            return await _productRepository.FindImageById(id);
        }

        public async Task RemoveImage(Image image)
        {
            await _productRepository.RemoveImage(image);
            await _productRepository.SaveChanges();
        }

        //public async Task InsertImage()
        //{
        //    //TODO
        //}

        //public async Task UpdateImage()
        //{
        //    //TODO
        //}


    }
}
