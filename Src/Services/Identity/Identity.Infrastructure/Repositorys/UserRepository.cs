using Identity.Domain;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IFreeSql freeSql) : base(freeSql)
        {
       

        }
    }
}
