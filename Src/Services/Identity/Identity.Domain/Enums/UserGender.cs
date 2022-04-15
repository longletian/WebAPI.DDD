using DomainBase.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain
{
   /// <summary>
   /// 性别枚举
   /// </summary>
   public  enum UserGender
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("男")]
        Male = 0,

        [Description("女")]
        Female = 1,

        [Description("未知")]
        Unknown = 2
    }
}
