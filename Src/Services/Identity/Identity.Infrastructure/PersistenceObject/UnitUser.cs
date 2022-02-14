using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_unit_user")]
    public class UnitUser : PersistenceObjectBase
    {
        public Guid UnitId { get; set; }

        public Guid UserId { get; set; }
    }
}
