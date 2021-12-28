using Domain.Interfaces.Repositories;
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
    public class PhoneRepository : IPhoneRepository
    {
        private readonly RegisterDbContext _context;

        public PhoneRepository(RegisterDbContext context)
        {
            _context = context;
        }

        public async Task<Phone> Find(Expression<Func<Phone, bool>> predicate)
        {
            return await _context.Phones.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<Phone> FindById(Guid id)
        {
            return await _context.Phones.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Insert(Phone phone)
        {
            await _context.AddAsync(phone);
        }

        public async Task Remove(Phone phone)
        {
            _context.Remove(phone);
            await Task.CompletedTask;
        }

        public async Task Update(Phone phone)
        {

            _context.Update(phone);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
