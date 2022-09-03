using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UnitUserQo: PageQueryQo
    {
        public Guid? UnitId { get; set; }
    }
}
