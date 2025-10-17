namespace Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;
public class ProjectGuestDailyReportUpdatedEvent : BaseEvent
{
    public ProjectGuestDailyReportUpdatedEvent(Entities.ProjectGuestDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectGuestDailyReport Item { get; }
}

