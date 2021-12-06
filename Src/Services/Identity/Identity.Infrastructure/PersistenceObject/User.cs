using Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.PersistenceObject
{
    public class User: UserEntity
    {
        public Guid AccountId { get; set; }
    }
}
