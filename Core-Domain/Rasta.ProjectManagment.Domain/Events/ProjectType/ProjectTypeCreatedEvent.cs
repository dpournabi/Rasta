namespace Rasta.ProjectManagment.Domain.Events.ProjectType;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectTypeCreatedEvent : BaseEvent
{
    public ProjectTypeCreatedEvent(ProjectType item)
    {
        Item = item;
    }

    public ProjectType Item { get; }
}
