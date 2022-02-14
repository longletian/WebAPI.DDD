using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class UserRepository : RepositoryBase<UserEntity,User>, IUserRepository
    {
        public UserRepository(IFreeSql freeSql) : base(freeSql)
        {
            
        }
    }
}
