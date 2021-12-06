using DomainBase;
using System;

namespace Identity.Infrastructure.PersistenceObject
{
    public class FileDataPermission : PersistenceObjectBase
    {
        public Guid FileDataId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
