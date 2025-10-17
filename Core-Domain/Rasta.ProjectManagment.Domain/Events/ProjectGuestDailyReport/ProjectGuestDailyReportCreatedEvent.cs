namespace Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;
public class ProjectGuestDailyReportCreatedEvent : BaseEvent
{
    public ProjectGuestDailyReportCreatedEvent(Entities.ProjectGuestDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectGuestDailyReport Item { get; }
}
