namespace Rasta.ProjectManagment.Domain.Events.ProjectType;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectTypeUpdatedEvent : BaseEvent
{
    public ProjectTypeUpdatedEvent(ProjectType item)
    {
        Item = item;
    }

    public ProjectType Item { get; }
}
