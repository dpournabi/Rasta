using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Common;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.EventHandlers;

public class ProjectAccidentDailyReportCreatedEventHandler : INotificationHandler<ProjectAccidentDailyReportCreatedEvent>
{
    private readonly ILogger<ProjectAccidentDailyReportCreatedEventHandler> _logger;
    public ProjectAccidentDailyReportCreatedEventHandler(ILogger<ProjectAccidentDailyReportCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectAccidentDailyReportCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
