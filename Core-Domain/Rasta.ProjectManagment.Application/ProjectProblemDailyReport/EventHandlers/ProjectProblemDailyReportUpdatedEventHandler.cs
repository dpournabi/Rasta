using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.EventHandlers;

public class ProjectProblemDailyReportUpdatedEventHandler : INotificationHandler<ProjectProblemDailyReportUpdatedEvent>
{
    private readonly ILogger<ProjectProblemDailyReportUpdatedEventHandler> _logger;
    public ProjectProblemDailyReportUpdatedEventHandler(ILogger<ProjectProblemDailyReportUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectProblemDailyReportUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
