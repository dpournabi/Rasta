namespace Rasta.ProjectManagment.Domain.Events.ProjectBreakdownHistory;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectBreakdownHistoryDeletedEvent : BaseEvent
{
    public ProjectWeek Item { get; }

    public ProjectBreakdownHistoryDeletedEvent(ProjectWeek item)
    {
        Item = item;
    }
}
