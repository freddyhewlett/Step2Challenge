using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Products;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RegisterDbContext _context;

        public CategoryRepository(RegisterDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories.Include(x => x.Products).Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> FindAll(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.Include(x => x.Category).Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Product>> ListCategories()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task Insert(Category category)
        {
            await _context.AddAsync(category);
        }

        public async Task Remove(Category category)
        {
            _context.Remove(category);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Category>> ToList()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task Update(Category category)
        {
            _context.Update(category);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Category> FindById(Guid id)
        {
            return await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> ListProducts()
        {
            return await _context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
        }

        public IQueryable<Category> SearchString(string search)
        {
            var categories = _context.Categories.Where(c => c.Name.Contains(search)).Include(x => x.Products);
            return categories;
        }

        public async Task<List<Category>> SortCategoryFilter(string sortOrder)
        {
            var products = from m in _context.Categories select m;
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(m => m.Name);
                    break;
                default:
                    products = products.OrderBy(m => m.Name);
                    break;
            }
            return await products.AsNoTracking().ToListAsync();
        }

        public async Task<PaginationViewModel<Category>> Pagination(int pageSize, int pageIndex, string query)
        {
            IPagedList<Category> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _context.Categories.Where(x => x.Name.Contains(query)).AsNoTracking().ToPagedListAsync(pageIndex, pageSize);
            }
            else
            {
                list = await _context.Categories
                                .Include(x => x.Products)
                                .AsNoTracking()
                                .ToPagedListAsync(pageIndex, pageSize);
            }

            return new PaginationViewModel<Category>()
            {
                List = list.ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
                TotalResult = list.TotalItemCount
            };
        }
    }
}
