namespace Rasta.ProjectManagment.Domain.Events.ProjectType;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectTypeDeletedEvent : BaseEvent
{
    public ProjectType Item { get; }

    public ProjectTypeDeletedEvent(ProjectType item)
    {
        Item = item;
    }
}
