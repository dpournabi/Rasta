namespace Rasta.ProjectManagment.Domain.Events.BasicInformation;
using Rasta.ProjectManagment.Domain.Entities;

public class BasicInformationCreatedEvent : BaseEvent
{
    public BasicInformationCreatedEvent(BasicInformation item)
    {
        Item = item;
    }

    public BasicInformation Item { get; }
}
