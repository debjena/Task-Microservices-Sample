using EventBusRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubTask_Api.Model
{
    public class SubTaskEvent : IntegrationEvent
    {
        public long id { get; set; }

        public string subtaskdesc { get; set; }

        public SubTaskEvent(long _id)
        {
            id = _id;
        }
    }
}
