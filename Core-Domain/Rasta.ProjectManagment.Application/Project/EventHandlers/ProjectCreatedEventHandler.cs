using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;

namespace Rasta.ProjectManagment.Application.Project.EventHandlers
{
    public class ProjectCreatedEventHandler : INotificationHandler<ProjectCreatedEvent>
    {
        private readonly ILogger<ProjectCreatedEventHandler> _logger;
        public ProjectCreatedEventHandler(ILogger<ProjectCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
