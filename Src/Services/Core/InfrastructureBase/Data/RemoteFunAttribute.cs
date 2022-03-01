using System;

namespace InfrastructureBase.Data
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    public class RemoteFunAttribute: Attribute
    {
        private string ServiceName;

    }
}
