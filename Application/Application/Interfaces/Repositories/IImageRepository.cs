using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Image> FindImage(Guid id);
        Task InsertImage(Image image);
        Task Update(Image image);
        Task RemoveImage(Image image);
        Task<int> SaveChanges();
        Task<string> FindImagePath(Guid id);
        Task<Image> FindById(Guid id)
    }
}
