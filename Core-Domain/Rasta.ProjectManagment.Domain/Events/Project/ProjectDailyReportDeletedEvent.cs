namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectDailyReportDeletedEvent : BaseEvent
{
    public ProjectExecutionDailyReport Item { get; }

    public ProjectDailyReportDeletedEvent(ProjectExecutionDailyReport item)
    {
        Item = item;
    }
}
