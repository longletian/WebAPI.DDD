using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure
{
    public class UserRole : PersistenceObjectBase
    {
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }
    }
}
