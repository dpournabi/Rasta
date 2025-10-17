namespace Rasta.ProjectManagment.Domain.Events.Property;
using Rasta.ProjectManagment.Domain.Entities;
public class PropertyCreatedEvent : BaseEvent
{
    public PropertyCreatedEvent(Property item)
    {
        Item = item;
    }

    public Property Item { get; }
}
