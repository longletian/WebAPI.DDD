using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase
{
    public abstract class RepositoryBase<Freesql, DomainModel> : IRepository<DomainModel> where Freesql : IFreeSql where DomainModel : Entity
    {
        private readonly Freesql freesql;
        public RepositoryBase(Freesql freesql)
        {
            this.freesql = freesql;
        }

        public virtual void Add(DomainModel t)
        {
            freesql.Insert(t);
        }

        public virtual async Task<bool> AnyAsync(object key = null)
        {
            return await freesql.Select<object>().AnyAsync(x => (x as Entity).Id == (long)key);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<DomainModel, bool>> condition)
        {
            return await freesql.Select<DomainModel>().AnyAsync(condition);
        }

        public virtual void Delete(DomainModel t)
        {
            freesql.Delete<DomainModel>(t);
        }

        public virtual void Delete(Expression<Func<DomainModel, bool>> condition)
        {
            freesql.Delete<DomainModel>().Where(condition);
        }

        public  async Task<DomainModel> GetAsync(object key = null)
        {
            return await freesql.Select<DomainModel>().FirstAsync();
        }

        public virtual IAsyncEnumerable<DomainModel> GetManyAsync(Guid[] key)
        {
            throw new NotImplementedException();
        }

        public virtual IAsyncEnumerable<DomainModel> GetManyAsync(Expression<Func<DomainModel, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(DomainModel t)
        {
            throw new NotImplementedException();
        }
    }
}
