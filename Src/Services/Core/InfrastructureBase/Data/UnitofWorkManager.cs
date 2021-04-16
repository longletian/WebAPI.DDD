using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public class UnitofWorkManager : IUnitOfWork
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
