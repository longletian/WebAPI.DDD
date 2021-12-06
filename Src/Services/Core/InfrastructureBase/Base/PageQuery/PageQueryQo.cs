namespace InfrastructureBase
{

   /// <summary>
   /// 数据查询实体
   /// </summary>
    public class PageQueryQo
    {
        public string KeyWord { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
