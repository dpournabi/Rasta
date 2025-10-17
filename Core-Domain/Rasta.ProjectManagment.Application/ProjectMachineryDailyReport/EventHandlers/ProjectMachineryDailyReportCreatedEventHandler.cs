using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.EventHandlers;

public class ProjectMachineryDailyReportCreatedEventHandler : INotificationHandler<ProjectMachineryDailyReportCreatedEvent>
{
    private readonly ILogger<ProjectMachineryDailyReportCreatedEventHandler> _logger;
    public ProjectMachineryDailyReportCreatedEventHandler(ILogger<ProjectMachineryDailyReportCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectMachineryDailyReportCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
