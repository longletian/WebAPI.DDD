using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class RoleRepository: RepositoryBase<RoleEntity,Role>, IRoleRepository
    {
        public RoleRepository(IFreeSql freeSql) :base(freeSql)
        { 
        }
        
    }
}
