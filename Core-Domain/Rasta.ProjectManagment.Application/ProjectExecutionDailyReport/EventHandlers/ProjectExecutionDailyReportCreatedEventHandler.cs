using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Application.Project.EventHandlers;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.EventHandlers
{
    public class ProjectExecutionDailyReportCreatedEventHandler : INotificationHandler<ProjectDailyReportCreatedEvent>
    {
        private readonly ILogger<ProjectCreatedEventHandler> _logger;
        public ProjectExecutionDailyReportCreatedEventHandler(ILogger<ProjectCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProjectDailyReportCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
