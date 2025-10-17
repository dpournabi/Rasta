using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.EventHandlers;

public class ProjectMachineryDailyReportUpdatedEventHandler : INotificationHandler<ProjectMachineryDailyReportUpdatedEvent>
{
    private readonly ILogger<ProjectMachineryDailyReportUpdatedEventHandler> _logger;
    public ProjectMachineryDailyReportUpdatedEventHandler(ILogger<ProjectMachineryDailyReportUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectMachineryDailyReportUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
