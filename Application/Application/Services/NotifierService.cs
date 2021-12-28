using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotifierService : INotifierService
    {
        private List<Notifier> errorList = new List<Notifier>();

        public NotifierService() { }

        public void AddError(string error)
        {
            errorList.Add(new Notifier(error));
        }

        public IEnumerable<Notifier> AllErrors()
        {
            return errorList;
        }

        public bool HasError()
        {
            return errorList.Any();
        }
    }
}
