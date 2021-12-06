using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure
{
    public class PermissionRepository : RepositoryBase<PermissionEntity, Permission>, IPermissionRepository
    {
        public PermissionRepository(IFreeSql freeSql) : base(freeSql)
        {

        }
    }
}
