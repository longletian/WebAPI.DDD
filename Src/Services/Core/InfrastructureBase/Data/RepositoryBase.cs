using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await freesql.Select<object>().AnyAsync(x => (x as Entity).Id == (Guid)key);
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

        public  virtual async Task<DomainModel> GetAsync(object key = null)
        {
            return await freesql.Select<DomainModel>(key).FirstAsync();
        }

        public virtual async Task<List<DomainModel>> GetManyAsync(Guid[] key)
        {
            var keys = key.ToList();
            return await freesql.Select<DomainModel>().Where((item) => keys.Contains(item.Id)).ToListAsync();
        }

        public virtual async Task<List<DomainModel>> GetManyAsync(Expression<Func<DomainModel, bool>> condition)
        {
            return await freesql.Select<DomainModel>().Where(condition).ToListAsync();
        }

        public virtual  void Update(DomainModel t)
        {
            freesql.Update<DomainModel>(t);
        }
    }
}
