namespace Rasta.ProjectManagment.Domain.Events.MeasureUnit;
using Rasta.ProjectManagment.Domain.Entities;
public class MeasureUnitDeletedEvent : BaseEvent
{
    public MeasureUnit Item { get; }

    public MeasureUnitDeletedEvent(MeasureUnit item)
    {
        Item = item;
    }
}
