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
    public abstract class RepositoryBase<TEntity, PersistenceObject> : BaseRepository<TEntity>,
        IRepository<TEntity> where TEntity : Entity where PersistenceObject: class
    {
        
        private readonly IFreeSql freeSql;

        protected RepositoryBase(IFreeSql freeSql) : base(freeSql, null, null)
        {
            this.freeSql = freeSql;
        }


        public virtual void Add(TEntity t)
        {
            this.freeSql.Insert<PersistenceObject>(t.Adapt<PersistenceObject>());
        }

        public Task<bool> AnyAsync(object key = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity t)
        {
            this.freeSql.Delete<PersistenceObject>(t.Adapt<PersistenceObject>());
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> condition)
        {
            this.freeSql.Delete<PersistenceObject>().Where(condition.Adapt<PersistenceObject>());
        }

        public Task<TEntity> GetAsync(object key = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetManyAsync(Guid[] key)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}