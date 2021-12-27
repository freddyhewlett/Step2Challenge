using Domain.Models.Products;
using Domain.Models.Suppliers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RegisterDbContext : DbContext
    {
        public RegisterDbContext(DbContextOptions<RegisterDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SupplierJuridical> JuridicalSuppliers { get; set; }
        public DbSet<SupplierPhysical> PhysicalSuppliers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("InsertDate") != null && x.Entity.GetType().GetProperty("UpdateDate") != null))
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property("InsertDate").CurrentValue = DateTime.Now;
                    entity.Property("UpdateDate").IsModified = false;
                }
                if (entity.State == EntityState.Modified)
                {
                    entity.Property("UpdateDate").CurrentValue = DateTime.Now;
                    entity.Property("InsertDate").IsModified = false;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
