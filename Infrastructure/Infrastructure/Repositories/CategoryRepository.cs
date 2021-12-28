using Domain.Interfaces.Repositories;
using Domain.Models.Products;
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
    public class CategoryRepository: ICategoryRepository
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
    }
}
