using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.EventHandlers;

public class ProjectGuestDailyReportDeletedEventHandler : INotificationHandler<ProjectGuestDailyReportDeletedEvent>
{
    private readonly ILogger<ProjectGuestDailyReportDeletedEventHandler> _logger;
    public ProjectGuestDailyReportDeletedEventHandler(ILogger<ProjectGuestDailyReportDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectGuestDailyReportDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
