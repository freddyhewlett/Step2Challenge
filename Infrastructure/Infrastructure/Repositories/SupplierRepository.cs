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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly RegisterDbContext _context;

        public SupplierRepository(RegisterDbContext context)
        {
            _context = context;
        }

        public async Task<SupplierPhysical> FindPhysical(Expression<Func<SupplierPhysical, bool>> predicate)
        {
            return await _context.PhysicalSuppliers.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<SupplierJuridical> FindJuridical(Expression<Func<SupplierJuridical, bool>> predicate)
        {
            return await _context.JuridicalSuppliers.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task InsertPhysical(SupplierPhysical supplier)
        {
            await _context.PhysicalSuppliers.AddAsync(supplier);
        }

        public async Task InsertJuridical(SupplierJuridical supplier)
        {
            await _context.JuridicalSuppliers.AddAsync(supplier);
        }

        public async Task RemovePhysical(SupplierPhysical supplier)
        {
            foreach (Phone phone in supplier.Phones)
            {
                _context.Phones.Remove(phone);
            }
            _context.Emails.Remove(supplier.Email);
            _context.Addresses.Remove(supplier.Address);
            _context.PhysicalSuppliers.Remove(supplier);
            await Task.CompletedTask;
        }

        public async Task RemoveJuridical(SupplierJuridical supplier)
        {
            foreach (Phone phone in supplier.Phones)
            {
                _context.Phones.Remove(phone);
            }
            _context.Emails.Remove(supplier.Email);
            _context.Addresses.Remove(supplier.Address);
            _context.JuridicalSuppliers.Remove(supplier);
            await Task.CompletedTask;
        }

        public async Task UpdatePhysical(SupplierPhysical supplier)
        {
            _context.PhysicalSuppliers.Update(supplier);
            await Task.CompletedTask;
        }

        public async Task UpdateJuridical(SupplierJuridical supplier)
        {
            await UpdateAddress(supplier.Address);
            await UpdateEmail(supplier.Email);
            _context.JuridicalSuppliers.Update(supplier);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<SupplierPhysical>> ToListPhysical()
        {
            //return await _context.PhysicalSuppliers.ToListAsync();
            return await _context.PhysicalSuppliers.Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).ToListAsync();
        }

        public async Task<IEnumerable<SupplierJuridical>> ToListJuridical()
        {
            //return await _context.JuridicalSuppliers.ToListAsync();
            return await _context.JuridicalSuppliers.Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).ToListAsync();
        }

        public IQueryable<SupplierPhysical> SearchPhysicalString(string search)
        {
            var suppliers = _context.PhysicalSuppliers.Where(s => s.FullName.Contains(search) || s.FantasyName.Contains(search));
            
            return suppliers;
        }

        public IQueryable<SupplierJuridical> SearchJuridicalString(string search)
        {
            var suppliers = _context.JuridicalSuppliers.Where(s => s.CompanyName.Contains(search) || s.FantasyName.Contains(search));

            return suppliers;
        }

        public async Task<SupplierPhysical> FindPhysicalById(Guid id)
        {
            return await _context.PhysicalSuppliers.AsNoTracking().Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SupplierJuridical> FindJuridicalById(Guid id)
        {
            return await _context.JuridicalSuppliers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> ProductListByPhysical(Guid id)
        {
            var supplier = await FindPhysicalById(id);
            return supplier.Products;
        }

        public async Task<IEnumerable<Product>> ProductListByJuridical(Guid id)
        {
            var supplier = await FindJuridicalById(id);
            return supplier.Products;
        }

        public async Task InsertPhone(Phone phone)
        {
            await _context.Phones.AddAsync(phone);
        }

        public async Task RemovePhone(Phone phone)
        {
            _context.Phones.Remove(phone);
            await Task.CompletedTask;
        }

        public async Task UpdatePhone(Phone phone)
        {
            _context.Phones.Update(phone);
            await Task.CompletedTask;
        }

        public async Task InsertAddress(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public async Task UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
            await Task.CompletedTask;
        }

        public async Task InsertEmail(Email email)
        {
            await _context.Emails.AddAsync(email);
        }

        public async Task UpdateEmail(Email email)
        {
            _context.Emails.Update(email);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
