using MediatR;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Domain.Events.BasicInformation;

namespace Rasta.ProjectManagment.Application.BasicInformation.EventHandlers;
public class BasicInformationCreatedEventHandler : INotificationHandler<BasicInformationCreatedEvent>
{
    private readonly ILogger<BasicInformationCreatedEventHandler> _logger;
    public BasicInformationCreatedEventHandler(ILogger<BasicInformationCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BasicInformationCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rasta.ProjectManagment.Application Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}