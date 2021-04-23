﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase.Event
{
    public abstract class Event 
    {
        public Event()
        {
            Id = Guid.NewGuid();
            CreateDateTime = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public DateTime CreateDateTime { get; }
    }
}