using DomainBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table("sys_operate_permission")]
   public class OperatePermission: PersistenceObjectBase
    {
        public Guid OperateId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
