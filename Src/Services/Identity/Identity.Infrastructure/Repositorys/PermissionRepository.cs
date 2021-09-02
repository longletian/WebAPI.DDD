using Identity.Domain;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(IFreeSql freeSql) : base(freeSql)
        {

        }
    }
}
