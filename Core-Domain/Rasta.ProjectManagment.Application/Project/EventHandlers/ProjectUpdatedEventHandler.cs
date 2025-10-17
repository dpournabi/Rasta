using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Domain.Events
{
    public class ProjectUpdatedEventHandler : INotificationHandler<ProjectUpdatedEvent>
    {
        private readonly ILogger<ProjectUpdatedEventHandler> _logger;
        public ProjectUpdatedEventHandler(ILogger<ProjectUpdatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ProjectUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
