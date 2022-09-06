using DomainBase;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UserGroupUserQo : PageQueryQo
    {
        public Guid? UserGroupId { get; set; }
    }
}
