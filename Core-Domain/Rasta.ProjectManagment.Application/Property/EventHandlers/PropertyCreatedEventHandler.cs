using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.Property;

namespace Rasta.ProjectManagment.Application.Property.EventHandlers;
public class PropertyCreatedEventHandler : INotificationHandler<PropertyCreatedEvent>
{
    private readonly ILogger<PropertyCreatedEventHandler> _logger;
    public PropertyCreatedEventHandler(ILogger<PropertyCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PropertyCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}


