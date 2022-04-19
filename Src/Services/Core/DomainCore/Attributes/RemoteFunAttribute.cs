using System;
using System.Net.Http;

namespace DomainBase.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public class RemoteFunAttribute: Attribute
    {
        public RemoteFunAttribute()
        {
            
        }
    }
}
