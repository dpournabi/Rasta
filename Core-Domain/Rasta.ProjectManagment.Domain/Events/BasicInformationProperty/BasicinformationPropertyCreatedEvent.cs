namespace Rasta.ProjectManagment.Domain.Events.BasicInformationProperty;
using Rasta.ProjectManagment.Domain.Entities;
public class BasicinformationPropertyCreatedEvent : BaseEvent
{
    public BasicinformationPropertyCreatedEvent(BasicInformationProperty item)
    {
        Item = item;
    }

    public BasicInformationProperty Item { get; }
}
