using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase
{
    public interface IUnitOfWork:IDisposable
    {
        bool Commit();
    }
}
