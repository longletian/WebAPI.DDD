using FreeSql.DataAnnotations;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.PersistenceObject
{   
    [Table(Name = "sys_menu")]
    public class Menu: MenuEntity
    {
    }
}
