using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.EventHandlers;

public class ProjectGuestDailyReportUpdatedEventHandler : INotificationHandler<ProjectGuestDailyReportUpdatedEvent>
{
    private readonly ILogger<ProjectGuestDailyReportUpdatedEventHandler> _logger;
    public ProjectGuestDailyReportUpdatedEventHandler(ILogger<ProjectGuestDailyReportUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectGuestDailyReportUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
