using Domain.Interfaces.Repositories;
using Domain.Models.Products;
using Domain.Models.Suppliers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RegisterDbContext _context;

        public ProductRepository(RegisterDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.Include(x => x.Category).Where(predicate).FirstOrDefaultAsync();
        }
        
        public async Task Insert(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task Remove(Product product)
        {
            foreach (Image item in product.Images)
            {
                _context.Images.Remove(item);
            }
            _context.Products.Remove(product);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> ToList()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task Update(Product product)
        {

            _context.Products.Update(product);
            await Task.CompletedTask;
        }

        public IQueryable<Product> SearchString(string search, Guid? selectedCategory)
        {
            var categoryID = selectedCategory.GetValueOrDefault();
            var products = _context.Products.Where(c => !selectedCategory.HasValue || c.CategoryId == categoryID);
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.Name.Contains(search));
            }
            return products;
        }

        public async Task<List<Product>> SortProductFilter(string sortOrder)
        {
            var products = from m in _context.Products select m;
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(m => m.Name);
                    break;
                case "Date":
                    products = products.OrderBy(m => m.InsertDate);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(m => m.InsertDate);
                    break;
                case "Quantity":
                    products = products.OrderBy(m => m.QuantityStock);
                    break;
                case "quantity_desc":
                    products = products.OrderByDescending(m => m.QuantityStock);
                    break;
                case "PriceSales":
                    products = products.OrderBy(m => m.PriceSales);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(m => m.PriceSales);
                    break;
                default:
                    products = products.OrderBy(m => m.Name);
                    break;
            }
            return await products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Category>> ListCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SupplierPhysical>> ListPhysicalSuppliers()
        {
            return await _context.PhysicalSuppliers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SupplierJuridical>> ListJuridicalSuppliers()
        {
            return await _context.JuridicalSuppliers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> ListAllSuppliersIdFantasy()
        {
            var juridical = await _context.JuridicalSuppliers.AsNoTracking().ToListAsync();
            var physical = await _context.PhysicalSuppliers.AsNoTracking().ToListAsync();
            List<Supplier> all = new List<Supplier>();
            foreach (var item in juridical)
            {
                all.Add(item);
            }
            foreach (var item in physical)
            {
                all.Add(item);
            }
            return all;
        }

        public async Task<string> FindImagePath(Guid id)
        {
            var imageList = await _context.Images.AsNoTracking().ToListAsync();
            var image = imageList.Find(x => x.Id == id);
            var path = image.ImagePath;
            return path;
        }

        public async Task<Product> FindById(Guid id)
        {
            return await _context.Products.Include(x => x.Images).Include(x => x.Supplier).Include(x => x.Category).Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task InsertImage(Image image)
        {
            await _context.Images.AddAsync(image);
        }

        public async Task RemoveImage(Image image)
        {
            _context.Images.Remove(image);
            await Task.CompletedTask;
        }

        public async Task UpdateImage(Image image)
        {

            _context.Images.Update(image);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
