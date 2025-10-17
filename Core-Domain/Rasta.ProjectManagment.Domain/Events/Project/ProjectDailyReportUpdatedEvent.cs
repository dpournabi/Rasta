namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectDailyReportUpdatedEvent : BaseEvent
{
    public ProjectDailyReportUpdatedEvent(ProjectExecutionDailyReport item)
    {
        Item = item;
    }

    public ProjectExecutionDailyReport Item { get; }
}
