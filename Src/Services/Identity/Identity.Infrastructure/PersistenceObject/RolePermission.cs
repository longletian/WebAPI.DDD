using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure
{
    public class RolePermission : PersistenceObjectBase
    {
        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
