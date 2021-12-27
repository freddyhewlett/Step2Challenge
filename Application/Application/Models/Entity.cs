using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime InsertDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();           
        }
    }
}
