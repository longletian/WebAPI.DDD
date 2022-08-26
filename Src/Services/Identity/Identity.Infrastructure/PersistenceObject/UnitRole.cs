using DomainBase;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_unit_role")]
    public class UnitRole: PersistenceObjectBase
    {
        public Guid UnitId { get; set; }

        public Guid RoleId { get; set; }
    }
}
