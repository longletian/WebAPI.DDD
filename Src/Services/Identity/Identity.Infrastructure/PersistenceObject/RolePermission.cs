using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;

namespace Identity.Infrastructure
{
    [Table(Name = "sys_role_permission")]
    public class RolePermission : PersistenceObjectBase
    {
        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
