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
    public class ImageRepository : IImageRepository
    {
        private readonly RegisterDbContext _context;

        public ImageRepository(RegisterDbContext context)
        {
            _context = context;
        }

        public async Task<Image> FindImage(Guid id)
        {
            return await _context.Images.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertImage(Image image)
        {
            await _context.AddAsync(image);
        }

        public async Task Update(Image image)
        {

            _context.Update(image);
            await Task.CompletedTask;
        }

        public async Task RemoveImage(Image image)
        {            
            _context.Remove(image);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<string> FindImagePath(Guid id)
        {
            var imageList = await _context.Images.AsNoTracking().ToListAsync();
            var image = imageList.Find(x => x.Id == id);
            var path = image.ImagePath;
            return path;
        }

        public async Task<Image> FindById(Guid id)
        {
            return await _context.Images.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
