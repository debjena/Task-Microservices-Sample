using EventBusRabbitMQ;
using Microsoft.Extensions.Logging;
using SubTask_Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubTask_Api.Model
{
    public class SubTaskEventHandler : IIntegrationEventHandler<SubTaskEvent>
    {
        private readonly IRepositoryFactory _repository;
        private readonly ILogger<SubTaskEventHandler> _logger;
        public SubTaskEventHandler(
           IRepositoryFactory repository,
           ILogger<SubTaskEventHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Handle(SubTaskEvent @event)
        {
            _logger.LogInformation("----- Subtask Handling integration event: {IntegrationEventId} at  ({@IntegrationEvent})", @event.Id,  @event);

            //await _repository.SubTask.GetSubTaskByIdAsync(@event.id);
            var sub = await _repository.SubTask.GetSubTaskByIdAsync(@event.id);
            sub.SubTaskDesc = @event.subtaskdesc;
            _repository.SubTask.Update(sub);
           await _repository.SubTask.SaveAsync();

        }
    }
}
