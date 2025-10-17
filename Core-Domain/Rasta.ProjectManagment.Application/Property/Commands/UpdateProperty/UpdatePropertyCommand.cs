using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Property;

namespace Rasta.ProjectManagment.Application.Property.Commands.UpdateProperty;
public class UpdatePropertyCommand : IRequest
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
}

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Properties.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.Name = request.Name;
        entity.Type = request.Type;

        entity.AddDomainEvent(new PropertyUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
