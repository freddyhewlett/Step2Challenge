using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface INotifierService
    {
        void AddError(string error);
        
        bool HasError();

        IEnumerable<Notifier> AllErrors();
    }
}
