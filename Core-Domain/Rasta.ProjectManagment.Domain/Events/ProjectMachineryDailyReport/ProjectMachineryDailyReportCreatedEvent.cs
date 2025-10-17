namespace Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;
public class ProjectMachineryDailyReportCreatedEvent : BaseEvent
{
    public ProjectMachineryDailyReportCreatedEvent(Entities.ProjectMachineryDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectMachineryDailyReport Item { get; }
}
