using MediatR;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.LookUp;

namespace Rasta.ProjectManagment.Application.LookUp.Commands.DeleteLookUp;
public record DeleteLookUpCommand(int Id) : IRequest;
public class DeleteLookUpCommandHandler : IRequestHandler<DeleteLookUpCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLookUpCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteLookUpCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LookUps
                .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        _context.LookUps.Remove(entity);

        entity.AddDomainEvent(new LookupDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
