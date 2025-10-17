using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Commands.DeleteProjectWorkBreakdown;
public record DeleteBatchProjectWorkBreakdownCommand(int ProjectId) : IRequest;
public class DeleteProjectWorkBreakdownCommandHandler : IRequestHandler<DeleteBatchProjectWorkBreakdownCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectWorkBreakdownCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBatchProjectWorkBreakdownCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.ProjectWorkBreakdowns
            .Where(x=>x.ProjectId==request.ProjectId).ToListAsync();

        if (entities == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        _context.ProjectWorkBreakdowns.RemoveRange(entities);

        foreach(var entity in entities)
            entity.AddDomainEvent(new ProjectWorkBreakdownDeletedEvent(entity));


        await _context.SaveChangesAsync(cancellationToken);
    }

}

