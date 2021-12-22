﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
