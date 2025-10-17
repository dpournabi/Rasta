namespace Rasta.ProjectManagment.Domain.Events.ProjectBreakdownHistory;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectBreakdownHistoryCreatedEvent : BaseEvent
{
    public ProjectBreakdownHistoryCreatedEvent(ProjectWeek item)
    {
        Item = item;
    }

    public ProjectWeek Item { get; }
}
