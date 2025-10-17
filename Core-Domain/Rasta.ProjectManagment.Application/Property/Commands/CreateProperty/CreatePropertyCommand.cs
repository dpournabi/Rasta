using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Property;

namespace Rasta.ProjectManagment.Application.Property.Commands.CreateProperty;

public class CreatePropertyCommand : IRequest<int>
{
    public required string Name { get; set; }
    public required string Type { get; set; }

    public static implicit operator Domain.Entities.Property(CreatePropertyCommand create)
    {
        return new Domain.Entities.Property
        {
            Name = create.Name,
            Type = create.Type
        };
    }
}
public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.Property)request;
        entity.AddDomainEvent(new PropertyCreatedEvent(entity));
        _context.Properties.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
