namespace Rasta.ProjectManagment.Domain.Events.ProjectTypeRatio;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectTypeRatioCreatedEvent : BaseEvent
{
    public ProjectTypeRatioCreatedEvent(ProjectTypeRatio item)
    {
        Item = item;
    }

    public ProjectTypeRatio Item { get; }
}
