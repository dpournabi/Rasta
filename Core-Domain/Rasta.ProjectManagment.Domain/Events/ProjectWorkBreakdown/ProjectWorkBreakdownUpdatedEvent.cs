namespace Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectWorkBreakdownUpdatedEvent : BaseEvent
{
    public ProjectWorkBreakdownUpdatedEvent(ProjectWorkBreakdown item)
    {
        Item = item;
    }

    public ProjectWorkBreakdown Item { get; }
}
