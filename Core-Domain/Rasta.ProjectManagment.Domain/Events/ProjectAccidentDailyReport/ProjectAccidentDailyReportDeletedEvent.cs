namespace Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectAccidentDailyReportDeletedEvent : BaseEvent
{
    public ProjectAccidentDailyReportDeletedEvent(Entities.ProjectAccidentDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectAccidentDailyReport Item { get; }
}
