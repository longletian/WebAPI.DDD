using DomainBase;
using System;

namespace Identity.Infrastructure.PersistenceObject
{
   public class OperatePermission: PersistenceObjectBase
    {
        public Guid OperateId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
