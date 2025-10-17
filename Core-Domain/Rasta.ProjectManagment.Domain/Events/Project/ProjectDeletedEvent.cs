namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectDeletedEvent : BaseEvent
{
    public Project Item { get; }

    public ProjectDeletedEvent(Project item)
    {
        Item = item;
    }
}
