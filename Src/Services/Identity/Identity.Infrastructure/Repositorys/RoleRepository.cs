using Identity.Domain;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class RoleRepository: RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IFreeSql freeSql) :base(freeSql)
        { 
        
        }
    }
}
