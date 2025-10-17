using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.EventHandlers;

public class ProjectProblemDailyReportCreatedEventHandler : INotificationHandler<ProjectProblemDailyReportCreatedEvent>
{
    private readonly ILogger<ProjectProblemDailyReportCreatedEventHandler> _logger;
    public ProjectProblemDailyReportCreatedEventHandler(ILogger<ProjectProblemDailyReportCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProjectProblemDailyReportCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
