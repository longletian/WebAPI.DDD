﻿using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.IntegrationEvents
{
    public record UserEvent(string UserName,int Age) : Event;
  
}
