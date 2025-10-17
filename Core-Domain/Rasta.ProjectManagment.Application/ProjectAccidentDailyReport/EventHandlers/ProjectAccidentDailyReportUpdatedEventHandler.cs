using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.EventHandlers;

public class ProjectAccidentDailyReportUpdatedEventHandler : INotificationHandler<ProjectAccidentDailyReportUpdatedEvent>
{
    private readonly ILogger<ProjectAccidentDailyReportUpdatedEventHandler> _logger;
    public ProjectAccidentDailyReportUpdatedEventHandler(ILogger<ProjectAccidentDailyReportUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectAccidentDailyReportUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
