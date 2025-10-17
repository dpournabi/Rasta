namespace Rasta.ProjectManagment.Domain.Events.Property;
using Rasta.ProjectManagment.Domain.Entities;
public class PropertyUpdatedEvent : BaseEvent
{
    public PropertyUpdatedEvent(Property item)
    {
        Item = item;
    }

    public Property Item { get; }
}
