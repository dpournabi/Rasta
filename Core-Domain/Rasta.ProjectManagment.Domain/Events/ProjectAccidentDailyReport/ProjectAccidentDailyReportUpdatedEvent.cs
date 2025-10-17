namespace Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectAccidentDailyReportUpdatedEvent : BaseEvent
{
    public ProjectAccidentDailyReportUpdatedEvent(Entities.ProjectAccidentDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectAccidentDailyReport Item { get; }
}

