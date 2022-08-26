using DomainBase;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mapster;

namespace InfrastructureBase
{
    public abstract class RepositoryBase<DomainModel, PersistenceObject> : BaseRepository<DomainModel>,
        IRepository<DomainModel> where DomainModel : Entity where PersistenceObject : class
    {
        
        private readonly IFreeSql freeSql;

        protected RepositoryBase(IFreeSql freeSql) : base(freeSql, null, null)
        {
            this.freeSql = freeSql;
        }

        public virtual void Add(DomainModel t)
        {
            this.freeSql.Insert<PersistenceObject>(t.MapTo<PersistenceObject>());
        }

        public virtual async Task<bool> AnyAsync(object key = null)
        {
            return await freeSql.Select<PersistenceObject>().AnyAsync(x => (x as Entity).Id == (Guid) key);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<DomainModel, bool>> condition)
        {
            return await freeSql.Select<DomainModel>().AnyAsync(condition);
        }

        public virtual async Task<DomainModel> GetAsync(object key = null)
        {
            return await freeSql.Select<DomainModel>(key).FirstAsync();
        }

        public virtual async Task<List<DomainModel>> GetManyAsync(Guid[] key)
        {
            var keys = key.ToList();
            return await freeSql.Select<DomainModel>().Where((item) => keys.Contains(item.Id)).ToListAsync();
        }

        public virtual async Task<List<DomainModel>> GetManyAsync(Expression<Func<DomainModel, bool>> condition)
        {
            return await freeSql.Select<DomainModel>().Where(condition).ToListAsync();
        }

    }
}