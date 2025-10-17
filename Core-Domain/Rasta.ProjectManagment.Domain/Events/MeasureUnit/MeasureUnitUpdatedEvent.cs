namespace Rasta.ProjectManagment.Domain.Events.MeasureUnit;
using Rasta.ProjectManagment.Domain.Entities;
public class MeasureUnitUpdatedEvent : BaseEvent
{
    public MeasureUnitUpdatedEvent(MeasureUnit item)
    {
        Item = item;
    }

    public MeasureUnit Item { get; }
}
