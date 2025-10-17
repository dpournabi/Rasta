namespace Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;
public class ProjectMachineryDailyReportUpdatedEvent : BaseEvent
{
    public ProjectMachineryDailyReportUpdatedEvent(Entities.ProjectMachineryDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectMachineryDailyReport Item { get; }
}

