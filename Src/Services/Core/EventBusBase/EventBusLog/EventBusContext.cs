using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusBase
{
    public class EventBusContext : DbContext
    {
        public EventBusContext(DbContextOptions<EventBusContext> options) : base(options)
        {

        }
    }
}
