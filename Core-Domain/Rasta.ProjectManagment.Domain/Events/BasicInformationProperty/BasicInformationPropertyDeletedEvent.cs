namespace Rasta.ProjectManagment.Domain.Events.BasicInformationProperty;
using Rasta.ProjectManagment.Domain.Entities;
public class BasicInformationPropertyDeletedEvent : BaseEvent
{
    public BasicInformationProperty Item { get; }

    public BasicInformationPropertyDeletedEvent(BasicInformationProperty item)
    {
        Item = item;
    }
}
