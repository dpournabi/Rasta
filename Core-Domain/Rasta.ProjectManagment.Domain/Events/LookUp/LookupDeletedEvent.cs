namespace Rasta.ProjectManagment.Domain.Events.LookUp;
using Rasta.ProjectManagment.Domain.Entities;
public class LookupDeletedEvent : BaseEvent
{
    public LookUp Item { get; }

    public LookupDeletedEvent(LookUp item)
    {
        Item = item;
    }
}
