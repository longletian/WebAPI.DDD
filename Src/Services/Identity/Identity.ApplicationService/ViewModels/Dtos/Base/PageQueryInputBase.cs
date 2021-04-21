using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{

   /// <summary>
   /// 数据查询实体
   /// </summary>
    public class PageQueryInputBase
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
