namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectDailyReportCreatedEvent : BaseEvent
{
    public ProjectDailyReportCreatedEvent(ProjectExecutionDailyReport item)
    {
        Item = item;
    }

    public ProjectExecutionDailyReport Item { get; }
}
