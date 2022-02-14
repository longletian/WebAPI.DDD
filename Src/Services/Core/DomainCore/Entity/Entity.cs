using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainBase
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Entity 
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
  
}
