using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.EventHandlers
{
    public class ProjectExecutionDailyReportUpdatedEventHandler : INotificationHandler<ProjectDailyReportUpdatedEvent>
    {
        private readonly ILogger<ProjectExecutionDailyReportUpdatedEventHandler> _logger;
        public ProjectExecutionDailyReportUpdatedEventHandler(ILogger<ProjectExecutionDailyReportUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProjectDailyReportUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
