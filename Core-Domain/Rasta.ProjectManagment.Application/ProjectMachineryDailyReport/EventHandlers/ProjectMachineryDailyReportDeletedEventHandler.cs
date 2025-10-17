using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.EventHandlers;

public class ProjectMachineryDailyReportDeletedEventHandler : INotificationHandler<ProjectMachineryDailyReportDeletedEvent>
{
    private readonly ILogger<ProjectMachineryDailyReportDeletedEventHandler> _logger;
    public ProjectMachineryDailyReportDeletedEventHandler(ILogger<ProjectMachineryDailyReportDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectMachineryDailyReportDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
