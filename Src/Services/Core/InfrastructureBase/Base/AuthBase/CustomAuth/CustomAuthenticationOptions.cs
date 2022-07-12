using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase.Base
{
    public class CustomAuthenticationOptions:AuthenticationSchemeOptions
    {
        public const string Scheme = "CustomAuth";
    }
}
