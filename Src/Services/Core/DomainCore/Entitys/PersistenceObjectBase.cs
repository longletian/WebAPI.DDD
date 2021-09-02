using System;
using System.ComponentModel.DataAnnotations;
namespace DomainBase
{
    /// <summary>
    /// 持久性对象
    /// </summary>
    public abstract class PersistenceObjectBase
    {
        public PersistenceObjectBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
