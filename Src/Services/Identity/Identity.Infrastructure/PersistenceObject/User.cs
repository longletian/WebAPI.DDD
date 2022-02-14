using Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_user")]
    public class User: UserEntity
    {
        public Guid AccountId { get; set; }
    }
}
