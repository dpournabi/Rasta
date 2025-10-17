using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.LookUp;

namespace Rasta.ProjectManagment.Application.LookUp.Commands.UpdateLookUp;
public class UpdateLookUpCommand : IRequest
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }
}
public class UpdateLookUpCommandHandler : IRequestHandler<UpdateLookUpCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateLookUpCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateLookUpCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LookUps.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.Title = request.Title;
        entity.Code = request.Code;
        entity.Type = request.Type;

        entity.AddDomainEvent(new LookUpUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
