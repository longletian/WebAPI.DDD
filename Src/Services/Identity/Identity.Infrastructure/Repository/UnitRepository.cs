using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;

namespace Identity.Infrastructure
{
    public class UnitRepository: RepositoryBase<UnitEntity,Unit>, IUnitRepository
    {
        public UnitRepository(IFreeSql freeSql) :base(freeSql)
        { 
        }
    }
}