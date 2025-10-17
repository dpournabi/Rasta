namespace Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectCreatedEvent : BaseEvent
{
    public ProjectCreatedEvent(Project item)
    {
        Item = item;
    }

    public Project Item { get; }
}
