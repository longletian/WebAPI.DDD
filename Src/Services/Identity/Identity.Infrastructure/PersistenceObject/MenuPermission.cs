using DomainBase;
using System;

namespace Identity.Infrastructure.PersistenceObject
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public class MenuPermission : PersistenceObjectBase
    {
        public Guid MenuId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
