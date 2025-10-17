namespace Rasta.ProjectManagment.Domain.Events.ProjectTypeRatio;
using Rasta.ProjectManagment.Domain.Entities;
public class ProjectTypeRatioUpdatedEvent : BaseEvent
{
    public ProjectTypeRatioUpdatedEvent(ProjectTypeRatio item)
    {
        Item = item;
    }

    public ProjectTypeRatio Item { get; }
}
