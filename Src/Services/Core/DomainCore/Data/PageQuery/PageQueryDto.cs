using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainBase
{
    public class PageQueryDto<T> where T:class
    {
        /// <summary>
        /// 集合
        /// </summary>
        public List<T> Items { get; }
        
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// 总码数
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// 码数
        /// </summary>
        public long TotalCount { get; }


        public PageQueryDto(IEnumerable<T> items, long count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items.ToList();
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}
