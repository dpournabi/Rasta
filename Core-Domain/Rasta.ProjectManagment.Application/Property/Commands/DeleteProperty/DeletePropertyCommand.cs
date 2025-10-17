using MediatR;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Property;

namespace Rasta.ProjectManagment.Application.Property.Commands.DeleteProperty;
public record DeletePropertyCommand(int Id) : IRequest;

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Properties
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        _context.Properties.Remove(entity);

        entity.AddDomainEvent(new PropertyDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
