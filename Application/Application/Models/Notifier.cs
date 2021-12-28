using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Notifier
    {
        public string Error { get; private set; }

        public Notifier(string error)
        {
            Error = error;
        }
    }
}
