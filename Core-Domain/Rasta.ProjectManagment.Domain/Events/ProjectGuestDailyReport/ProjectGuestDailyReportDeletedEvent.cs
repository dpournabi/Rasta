namespace Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;
public class ProjectGuestDailyReportDeletedEvent : BaseEvent
{
    public ProjectGuestDailyReportDeletedEvent(Entities.ProjectGuestDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectGuestDailyReport Item { get; }
}
