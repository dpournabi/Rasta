namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectUpdatedEvent : BaseEvent
{
    public ProjectUpdatedEvent(Project item)
    {
        Item = item;
    }

    public Project Item { get; }
}
