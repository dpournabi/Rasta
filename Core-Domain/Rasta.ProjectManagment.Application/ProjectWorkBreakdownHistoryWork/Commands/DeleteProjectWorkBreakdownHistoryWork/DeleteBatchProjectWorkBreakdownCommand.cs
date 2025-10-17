using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Commands.DeleteProjectWorkBreakdownHistoryWork;
public record DeleteProjectWorkBreakdownHistoryWorkCommand(long ProjectWorkBreakdownHistoryWorkId) : IRequest;
public class DeleteProjectWorkBreakdownCommandHandler : IRequestHandler<DeleteProjectWorkBreakdownHistoryWorkCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectWorkBreakdownCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProjectWorkBreakdownHistoryWorkCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectWorkBreakdowns
            .FirstOrDefaultAsync(x=>x.Id==request.ProjectWorkBreakdownHistoryWorkId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectWorkBreakdownHistoryWorkId);
        }

        _context.ProjectWorkBreakdowns.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

}

