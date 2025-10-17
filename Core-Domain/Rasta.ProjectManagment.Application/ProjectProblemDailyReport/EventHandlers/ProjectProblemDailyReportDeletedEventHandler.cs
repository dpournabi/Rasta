using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.EventHandlers;

public class ProjectProblemDailyReportDeletedEventHandler : INotificationHandler<ProjectProblemDailyReportDeletedEvent>
{
    private readonly ILogger<ProjectProblemDailyReportDeletedEventHandler> _logger;
    public ProjectProblemDailyReportDeletedEventHandler(ILogger<ProjectProblemDailyReportDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectProblemDailyReportDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
