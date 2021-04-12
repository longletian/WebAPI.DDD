using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase
{
    /// <summary>
    /// 实体
    /// </summary>
    [Serializable]
    public class Entity : Entity<long>
    {

    }
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true, Position = 1)]
        public TPrimaryKey Id { get; set; }
    }


    /// <summary>
    /// 实现根实体（将增删改基础的信息拆分开来）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FullEntity<T> : Entity<T>, IUpdateAuditEntity, IDeleteAduitEntity, ICreateAduitEntity
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        [Column(Position = -2)]
        public long? UpdateUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(Position = -1)]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(Position = -5)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 删除人id
        /// </summary>
        [Column(Position = -4)]
        public long? DeleteUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(Position = -3)]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>

        [Column(Position = -7)]
        public long CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Position = -6)]
        public DateTime CreateTime { get; set; }
    }

    [Serializable]
    public class FullEntity : FullEntity<long>
    {

    }
}
