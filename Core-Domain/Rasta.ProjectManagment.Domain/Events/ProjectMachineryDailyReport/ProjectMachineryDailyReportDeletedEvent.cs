namespace Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;
public class ProjectMachineryDailyReportDeletedEvent : BaseEvent
{
    public ProjectMachineryDailyReportDeletedEvent(Entities.ProjectMachineryDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectMachineryDailyReport Item { get; }
}
