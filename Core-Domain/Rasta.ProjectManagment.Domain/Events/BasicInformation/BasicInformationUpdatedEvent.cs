namespace Rasta.ProjectManagment.Domain.Events.BasicInformation;
using Rasta.ProjectManagment.Domain.Entities;

public class BasicInformationUpdatedEvent : BaseEvent
{
    public BasicInformationUpdatedEvent(BasicInformation item)
    {
        Item = item;
    }

    public BasicInformation Item { get; }
}
