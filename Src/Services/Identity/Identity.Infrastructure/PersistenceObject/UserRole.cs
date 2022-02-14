using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;

namespace Identity.Infrastructure
{
    [Table(Name = "sys_user_role")]
    public class UserRole : PersistenceObjectBase
    {
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }
    }
}
