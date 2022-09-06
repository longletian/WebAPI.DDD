using System.Collections.Generic;

namespace DomainBase
{
    public class PageQueryDto<T> where T:class
    {
        public PageQueryDto(IEnumerable<T> data, long total)
        {
            this.Data = data;
            this.PageTotal = total;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public long PageTotal { get; set; }
    }
}
