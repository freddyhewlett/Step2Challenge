using Domain.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IPhoneRepository
    {
        Task<Phone> Find(Expression<Func<Phone, bool>> predicate);
        Task<Phone> FindById(Guid id);
        Task Insert(Phone phone);
        Task Remove(Phone phone);
        Task Update(Phone phone);
        Task<int> SaveChanges();
    }
}
