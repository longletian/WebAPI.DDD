using DomainBase;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_usergroup_user")]
    public class UserGroupUser: PersistenceObjectBase
    {
        public Guid UserId { get; set; }

        public Guid UserGroupId { get; set; }
    }
}
