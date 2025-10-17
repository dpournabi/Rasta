namespace Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;
public class ProjectProblemDailyReportDeletedEvent : BaseEvent
{
    public ProjectProblemDailyReportDeletedEvent(Entities.ProjectProblemDailyReport item)
    {
        Item = item;
    }

    public Entities.ProjectProblemDailyReport Item { get; }
}
