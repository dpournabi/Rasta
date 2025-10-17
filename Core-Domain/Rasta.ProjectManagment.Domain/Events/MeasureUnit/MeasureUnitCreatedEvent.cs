namespace Rasta.ProjectManagment.Domain.Events.MeasureUnit;
using Rasta.ProjectManagment.Domain.Entities;
public class MeasureUnitCreatedEvent : BaseEvent
{
    public MeasureUnitCreatedEvent(MeasureUnit item)
    {
        Item = item;
    }

    public MeasureUnit Item { get; }
}
