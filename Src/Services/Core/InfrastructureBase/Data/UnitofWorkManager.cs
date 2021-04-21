using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    /// <summary>
    /// 定义工作单元模式（freesql内部有自定义的工作单元事务）
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWork
    {
        public bool Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
