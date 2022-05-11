using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.IntegrationEvents
{
    public class UserEvent : Event
    {
        public Guid Id { get; private set; }

        public string UserName { get; private set; }

        public int Age { get; private set; }

        public UserEvent(Guid id, string userName, int age)
        {
            this.Id = Id;
            this.UserName = userName;
            this.Age = age;
        }
    }
}
