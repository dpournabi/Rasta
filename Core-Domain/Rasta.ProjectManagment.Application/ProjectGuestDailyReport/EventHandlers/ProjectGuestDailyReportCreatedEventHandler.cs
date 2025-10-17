using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.EventHandlers;

public class ProjectGuestDailyReportCreatedEventHandler : INotificationHandler<ProjectGuestDailyReportCreatedEvent>
{
    private readonly ILogger<ProjectGuestDailyReportCreatedEventHandler> _logger;
    public ProjectGuestDailyReportCreatedEventHandler(ILogger<ProjectGuestDailyReportCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectGuestDailyReportCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
