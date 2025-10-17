namespace Rasta.ProjectManagment.Domain.Events.LookUp;
using Rasta.ProjectManagment.Domain.Entities;
public class LookUpCreatedEvent : BaseEvent
{
    public LookUpCreatedEvent(LookUp item)
    {
        Item = item;
    }

    public LookUp Item { get; }
}
