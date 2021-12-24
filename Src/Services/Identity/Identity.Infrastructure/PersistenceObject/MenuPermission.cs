using DomainBase;
using System;
using FreeSql.DataAnnotations;

namespace Identity.Infrastructure.PersistenceObject
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    [Table(Name = "sys_menu_permission")]
    public class MenuPermission : PersistenceObjectBase
    {
        public Guid MenuId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
