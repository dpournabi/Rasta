namespace Rasta.ProjectManagment.Domain.Events.BasicInformationProperty;
using Rasta.ProjectManagment.Domain.Entities;
public class BasicInformationPropertyUpdatedEvent : BaseEvent
{
    public BasicInformationPropertyUpdatedEvent(BasicInformationProperty item)
    {
        Item = item;
    }

    public BasicInformationProperty Item { get; }
}
