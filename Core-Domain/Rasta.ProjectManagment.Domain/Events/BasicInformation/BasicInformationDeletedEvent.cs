namespace Rasta.ProjectManagment.Domain.Events.BasicInformation;
using Rasta.ProjectManagment.Domain.Entities;
public class BasicInformationDeletedEvent : BaseEvent
{
    public BasicInformationDeletedEvent(BasicInformation item)
    {
        Item = item;
    }

    public BasicInformation Item { get; }
}
