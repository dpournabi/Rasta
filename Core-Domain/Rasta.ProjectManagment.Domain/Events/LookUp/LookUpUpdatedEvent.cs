namespace Rasta.ProjectManagment.Domain.Events.LookUp;
using Rasta.ProjectManagment.Domain.Entities;
public class LookUpUpdatedEvent : BaseEvent
{
    public LookUpUpdatedEvent(LookUp item)
    {
        Item = item;
    }

    public LookUp Item { get; }
}
