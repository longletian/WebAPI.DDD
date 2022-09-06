using Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class RegisterAccountInput
    {

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        ///<summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public UserGender? Gender { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 户籍地址
        /// </summary>
        public AddressEntity Address { get; set; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        public Guid? UserRoleId { get; set; }
    }
}
