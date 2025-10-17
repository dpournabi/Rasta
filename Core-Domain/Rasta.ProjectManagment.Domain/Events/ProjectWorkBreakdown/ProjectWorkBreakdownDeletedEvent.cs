namespace Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectWorkBreakdownDeletedEvent : BaseEvent
{
    public ProjectWorkBreakdown Item { get; }

    public ProjectWorkBreakdownDeletedEvent(ProjectWorkBreakdown item)
    {
        Item = item;
    }
}
