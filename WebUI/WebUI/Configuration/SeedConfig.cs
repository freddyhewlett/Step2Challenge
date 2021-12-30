using Bogus;
using Domain.Models.Products;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Configuration
{
    public class SeedConfig
    {
        private readonly RegisterDbContext _context;

        public SeedConfig(RegisterDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Categories.Any())
            {
                return;
            }

            var category = new Faker<Category>("pt_BR").CustomInstantiator(x => new Category(new Faker().Commerce.Categories(15).First()));


            List<Category> categoryList = category.Generate(15);

            _context.Categories.AddRange(categoryList);
            _context.SaveChangesAsync();
        }
    }
}
