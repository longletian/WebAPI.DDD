using System.Collections.Generic;

namespace InfrastructureBase
{
    public class PageQueryOutput<T> where T:class
    {
        public PageQueryOutput(List<T> data,int total)
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
