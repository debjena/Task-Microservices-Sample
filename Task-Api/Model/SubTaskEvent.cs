using EventBusRabbitMQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Repo_Pattern.Model
{
    public class SubTaskEvent : IntegrationEvent
    {
        public long id { get; set; }

        public string subtaskdesc { get; set; }

        public SubTaskEvent(long _id,string _desc)
        {
            id = _id;
            subtaskdesc = _desc;
        }

    }
}
