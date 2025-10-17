namespace Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectAccidentDailyReportCreatedEvent : BaseEvent
{
    public ProjectAccidentDailyReportCreatedEvent(Entities.ProjectAccidentDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectAccidentDailyReport Item { get; }
}
