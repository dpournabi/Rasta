using MediatR;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectTypeRatio;

namespace Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.DeleteProjectTypeRatio;
public record DeleteProjectTypeRatioCommand(int Id) : IRequest;
public class DeleteProjectTypeRatioCommandHandler : IRequestHandler<DeleteProjectTypeRatioCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectTypeRatioCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProjectTypeRatioCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectTypeRatios
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        _context.ProjectTypeRatios.Remove(entity);

        entity.AddDomainEvent(new ProjectTypeRatioDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
