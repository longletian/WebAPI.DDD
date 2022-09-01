using DomainBase;
using FreeSql.DataAnnotations;
using Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.PersistenceObject
{
    [Table(Name = "sys_action_log")]
    public class ActionLog: ActionLogEntity
    {
        
    }
}
