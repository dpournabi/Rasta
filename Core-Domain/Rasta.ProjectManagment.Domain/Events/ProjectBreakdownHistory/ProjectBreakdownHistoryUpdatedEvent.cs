namespace Rasta.ProjectManagment.Domain.Events.ProjectBreakdownHistory;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectBreakdownHistoryUpdatedEvent : BaseEvent
{
    public ProjectBreakdownHistoryUpdatedEvent(ProjectWeek item)
    {
        Item = item;
    }

    public ProjectWeek Item { get; }
}
