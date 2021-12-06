using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.PersistenceObject
{
    public class UnitUser : PersistenceObjectBase
    {
        public Guid UnitId { get; set; }

        public Guid UserId { get; set; }
    }
}
