namespace Rasta.ProjectManagment.Domain.Events.Project;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectCompletedEvent : BaseEvent
{
    public ProjectCompletedEvent(Project item)
    {
        Item = item;
    }

    public Project Item { get; }
}
