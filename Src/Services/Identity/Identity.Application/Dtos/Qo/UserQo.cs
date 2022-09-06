using DomainBase;

namespace Identity.Application
{
    /// <summary>
    /// 用户列表查询
    /// DTO代表服务层需要接收的数据和返回的数据。
    /// VO代表展示层需要显示的数据。
    /// PO
    /// </summary>
    public class UserQo : PageQueryQo
    {
        public int? UserGroupId { get; set; }

        public int? UnitId { get; set; }

    }
}
