using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class PermissionRepository : RepositoryBase<PermissionEntity, Permission>, IPermissionRepository
    {
        public PermissionRepository(IFreeSql freeSql) : base(freeSql)
        {
        }
    }
}
