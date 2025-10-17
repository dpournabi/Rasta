namespace Rasta.ProjectManagment.Domain.Events.Property;
using Rasta.ProjectManagment.Domain.Entities;
public class PropertyDeletedEvent : BaseEvent
{
    public Property Item { get; }

    public PropertyDeletedEvent(Property item)
    {
        Item = item;
    }
}
