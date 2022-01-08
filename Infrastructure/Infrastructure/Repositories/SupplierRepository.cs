using Domain.Interfaces.Repositories;
using Domain.Models;
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
using X.PagedList;

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
            _context.JuridicalSuppliers.Update(supplier);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<SupplierPhysical>> ToListPhysical()
        {
            return await _context.PhysicalSuppliers.Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).ToListAsync();
        }

        public async Task<IEnumerable<SupplierJuridical>> ToListJuridical()
        {
            return await _context.JuridicalSuppliers.Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).ToListAsync();
        }

        public IQueryable<SupplierPhysical> SearchPhysicalString(string search)
        {
            var suppliers = _context.PhysicalSuppliers.Where(s => s.FullName.Contains(search) || s.FantasyName.Contains(search))
                                                        .Include(x => x.Address)
                                                        .Include(x => x.Email)
                                                        .Include(x => x.Phones)
                                                        .Include(x => x.Products);
            return suppliers;
        }

        public async Task<List<SupplierPhysical>> SortPhysicalFilter(string sortOrder)
        {
            var physicalSuppliers = from m in _context.PhysicalSuppliers select m;
            switch (sortOrder)
            {
                case "name_desc":
                    physicalSuppliers = physicalSuppliers.OrderByDescending(m => m.FullName);
                    break;
                case "Date":
                    physicalSuppliers = physicalSuppliers.OrderBy(m => m.InsertDate);
                    break;
                case "date_desc":
                    physicalSuppliers = physicalSuppliers.OrderByDescending(m => m.InsertDate);
                    break;
                default:
                    physicalSuppliers = physicalSuppliers.OrderBy(m => m.FullName);
                    break;
            }
            return await physicalSuppliers.AsNoTracking().ToListAsync();
        }

        public IQueryable<SupplierJuridical> SearchJuridicalString(string search)
        {
            var suppliers = _context.JuridicalSuppliers.Where(s => s.CompanyName.Contains(search) || s.FantasyName.Contains(search))
                                                        .Include(x => x.Address)
                                                        .Include(x => x.Email)
                                                        .Include(x => x.Phones)
                                                        .Include(x => x.Products);

            return suppliers;
        }

        public async Task<List<SupplierJuridical>> SortJuridicalFilter(string sortOrder)
        {
            var juridicalSuppliers = from m in _context.JuridicalSuppliers select m;
            switch (sortOrder)
            {
                case "name_desc":
                    juridicalSuppliers = juridicalSuppliers.OrderByDescending(m => m.CompanyName);
                    break;
                case "Date":
                    juridicalSuppliers = juridicalSuppliers.OrderBy(m => m.InsertDate);
                    break;
                case "date_desc":
                    juridicalSuppliers = juridicalSuppliers.OrderByDescending(m => m.InsertDate);
                    break;
                default:
                    juridicalSuppliers = juridicalSuppliers.OrderBy(m => m.CompanyName);
                    break;
            }
            return await juridicalSuppliers.AsNoTracking().ToListAsync();
        }

        public async Task<SupplierPhysical> FindPhysicalById(Guid id)
        {
            return await _context.PhysicalSuppliers.AsNoTracking().Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SupplierJuridical> FindJuridicalById(Guid id)
        {
            return await _context.JuridicalSuppliers.AsNoTracking().Include(x => x.Address).Include(x => x.Email).Include(x => x.Phones).Where(x => x.Id == id).FirstOrDefaultAsync();
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

        public async Task<PaginationViewModel<SupplierPhysical>> PaginationPhysical(int pageSize, int pageIndex, string query)
        {
            IPagedList<SupplierPhysical> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _context.PhysicalSuppliers.Where(x => x.Email.EmailAddress.Contains(query)).AsNoTracking().ToPagedListAsync(pageIndex, pageSize);
            }
            else
            {
                list = await _context.PhysicalSuppliers.Include(x => x.Phones)
                                .Include(x => x.Email)
                                .Include(x => x.Address)
                                .AsNoTracking()
                                .ToPagedListAsync(pageIndex, pageSize);
            }

            return new PaginationViewModel<SupplierPhysical>()
            {
                List = list.ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
                TotalResult = list.TotalItemCount
            };
        }

        public async Task<PaginationViewModel<SupplierJuridical>> PaginationJuridical(int pageSize, int pageIndex, string query)
        {
            IPagedList<SupplierJuridical> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _context.JuridicalSuppliers.Where(x => x.Email.EmailAddress.Contains(query)).AsNoTracking().ToPagedListAsync(pageIndex, pageSize);
            }
            else
            {
                list = await _context.JuridicalSuppliers.Include(x => x.Phones)
                                .Include(x => x.Email)
                                .Include(x => x.Address)
                                .AsNoTracking()
                                .ToPagedListAsync(pageIndex, pageSize);
            }

            return new PaginationViewModel<SupplierJuridical>()
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
