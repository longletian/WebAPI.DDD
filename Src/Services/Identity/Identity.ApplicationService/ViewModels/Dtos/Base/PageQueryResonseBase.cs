using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{

    public class PageQueryResonseBase<T>
    {
        public PageQueryResonseBase(List<T> data,int total)
        {
            this.Data = data;
            this.PageTotal = total;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int PageTotal { get; set; }
    }
}
