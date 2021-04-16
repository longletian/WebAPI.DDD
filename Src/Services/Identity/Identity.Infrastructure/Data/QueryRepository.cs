using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public class QueryRepository<DomainModel> : IQueryRepository<DomainModel> where DomainModel : IAggregateRoot
    {

    }
}
