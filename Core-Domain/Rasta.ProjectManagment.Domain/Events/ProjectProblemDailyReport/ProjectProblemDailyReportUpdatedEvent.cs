namespace Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;
public class ProjectProblemDailyReportUpdatedEvent : BaseEvent
{
    public ProjectProblemDailyReportUpdatedEvent(Entities.ProjectProblemDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectProblemDailyReport Item { get; }
}

