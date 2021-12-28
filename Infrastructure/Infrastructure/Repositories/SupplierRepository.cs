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
            await _context.AddAsync(supplier);
        }

        public async Task InsertJuridical(SupplierJuridical supplier)
        {
            await _context.AddAsync(supplier);
        }

        public async Task RemovePhysical(SupplierPhysical supplier)
        {
            _context.Remove(supplier);
            await Task.CompletedTask;
        }

        public async Task RemoveJuridical(SupplierJuridical supplier)
        {
            _context.Remove(supplier);
            await Task.CompletedTask;
        }

        public async Task UpdatePhysical(SupplierPhysical supplier)
        {

            _context.Update(supplier);
            await Task.CompletedTask;
        }

        public async Task UpdateJuridical(SupplierJuridical supplier)
        {

            _context.Update(supplier);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<SupplierPhysical>> ToListPhysical()
        {
            return await _context.PhysicalSuppliers.ToListAsync();
        }

        public async Task<IEnumerable<SupplierJuridical>> ToListJuridical()
        {
            return await _context.JuridicalSuppliers.ToListAsync();
        }

        public IQueryable<SupplierPhysical> SearchPhysicalString(string search, Guid? id)
        {
            var supplierID = id.GetValueOrDefault();
            var suppliers = _context.PhysicalSuppliers.Where(c => !id.HasValue || c.Id == supplierID);
            if (!String.IsNullOrEmpty(search))
            {
                suppliers = suppliers.Where(s => s.FullName.Contains(search) || s.FantasyName.Contains(search));
            }
            return suppliers;
        }

        public IQueryable<SupplierJuridical> SearchJuridicalString(string search, Guid? id)
        {
            var supplierID = id.GetValueOrDefault();
            var suppliers = _context.JuridicalSuppliers.Where(c => !id.HasValue || c.Id == supplierID);
            if (!String.IsNullOrEmpty(search))
            {
                suppliers = suppliers.Where(s => s.CompanyName.Contains(search) || s.FantasyName.Contains(search));
            }
            return suppliers;
        }

        public async Task<SupplierPhysical> FindPhysicalById(Guid id)
        {
            return await _context.PhysicalSuppliers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SupplierJuridical> FindJuridicalById(Guid id)
        {
            return await _context.JuridicalSuppliers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }        

        public async Task InsertPhone(Phone phone)
        {
            await _context.AddAsync(phone);
        }

        public async Task RemovePhone(Phone phone)
        {
            _context.Remove(phone);
            await Task.CompletedTask;
        }

        public async Task UpdatePhone(Phone phone)
        {
            _context.Update(phone);
            await Task.CompletedTask;
        }

        public async Task UpdateAddress(Address address)
        {
            _context.Update(address);
            await Task.CompletedTask;
        }

        public async Task UpdateEmail(Email email)
        {
            _context.Update(email);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
