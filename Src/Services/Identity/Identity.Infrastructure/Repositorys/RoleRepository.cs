using Identity.Domain;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class RoleRepository: RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(IFreeSql freeSql) :base(freeSql)
        { 
        
        }
    }
}
