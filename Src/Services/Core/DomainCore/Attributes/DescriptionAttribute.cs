using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBase.Attributes
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field,AllowMultiple =true)]
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }
        public string Description { get; private set; }
    }
}
