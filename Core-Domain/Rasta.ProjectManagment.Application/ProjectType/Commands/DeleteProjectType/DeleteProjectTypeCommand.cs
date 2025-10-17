using MediatR;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectType;

namespace Rasta.ProjectManagment.Application.ProjectType.Commands.DeleteProjectType;
public record DeleteProjectTypeCommand(int Id) : IRequest;
public class DeleteProjectTypeCommandHandler : IRequestHandler<DeleteProjectTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProjectTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.projectTypes
                .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        _context.projectTypes.Remove(entity);

        entity.AddDomainEvent(new ProjectTypeDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
