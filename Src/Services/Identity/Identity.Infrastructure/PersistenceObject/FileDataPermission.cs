using DomainBase;
using System;
using FreeSql.DataAnnotations;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_file_permission")]
    public class FileDataPermission : PersistenceObjectBase
    {
        public Guid FileDataId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
