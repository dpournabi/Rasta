namespace Rasta.ProjectManagment.Domain.Events.ProjectTypeRatio;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectTypeRatioDeletedEvent : BaseEvent
{
    public ProjectTypeRatio Item { get; }

    public ProjectTypeRatioDeletedEvent(ProjectTypeRatio item)
    {
        Item = item;
    }
}
