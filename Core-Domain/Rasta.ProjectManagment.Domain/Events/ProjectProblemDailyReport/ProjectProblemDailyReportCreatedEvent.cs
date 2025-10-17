namespace Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;
public class ProjectProblemDailyReportCreatedEvent : BaseEvent
{
    public ProjectProblemDailyReportCreatedEvent(Entities.ProjectProblemDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectProblemDailyReport Item { get; }
}
