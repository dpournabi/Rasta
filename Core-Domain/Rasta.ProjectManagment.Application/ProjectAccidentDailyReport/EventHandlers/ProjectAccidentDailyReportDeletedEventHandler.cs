using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.EventHandlers;

public class ProjectAccidentDailyReportDeletedEventHandler : INotificationHandler<ProjectAccidentDailyReportDeletedEvent>
{
    private readonly ILogger<ProjectAccidentDailyReportDeletedEventHandler> _logger;
    public ProjectAccidentDailyReportDeletedEventHandler(ILogger<ProjectAccidentDailyReportDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectAccidentDailyReportDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
