using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events.BasicInformationProperty;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.CreateBasicInformationProperty;
public class CreateBasicInformationPropertyCommand : IRequest<int>
{
    public required int BasicInformationId { get; set; }
    public required int PropertyId { get; set; }
    public required string Value { get; set; }

    public static implicit operator Domain.Entities.BasicInformationProperty(CreateBasicInformationPropertyCommand create)
    {
        return new Domain.Entities.BasicInformationProperty
        {
            BasicInformationId = create.BasicInformationId,
            PropertyId = create.PropertyId,
            Value = create.Value
        };
    }
}
public class CreateBasicInformationPropertyCommandHandler : IRequestHandler<CreateBasicInformationPropertyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBasicInformationPropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBasicInformationPropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.BasicInformationProperty)request;
        entity.AddDomainEvent(new BasicinformationPropertyCreatedEvent(entity));
        _context.BasicInformationProperties.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

