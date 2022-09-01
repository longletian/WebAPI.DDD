using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain
{
    public class ActionLogEntity: Entity
    {
        public int ActionType { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Description { get; set; }

        public Guid UserId { get; set; }

        public DateTime? GmtCreate { get; set; }

    }
}
