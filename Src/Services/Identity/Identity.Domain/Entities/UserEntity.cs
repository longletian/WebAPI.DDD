using DomainBase;
using Identity.Domain.Enums;
using System;

namespace Identity.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserEntity : Entity
    {
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string ImagePath { get; set; }
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
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }
        /// <summary>
        /// 户籍地址
        /// </summary>
        public AddressEntity Address { get; set; }
    }
}
