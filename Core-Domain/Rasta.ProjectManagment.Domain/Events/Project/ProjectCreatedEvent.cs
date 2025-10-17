namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectWorkBreakdownCreatedEvent : BaseEvent
{
    public ProjectWorkBreakdownCreatedEvent(ProjectWorkBreakdown item)
    {
        Item = item;
    }

    public ProjectWorkBreakdown Item { get; }
}
