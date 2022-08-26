using DomainBase;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_usergroup_role")]
    public class UserGroupRole : PersistenceObjectBase
    {
        public Guid UserGroupId { get; set; }

        public Guid RoleId { get; set; }
    }
}
