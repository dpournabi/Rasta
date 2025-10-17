using MediatR;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.MeasureUnit;

namespace Rasta.ProjectManagment.Application.MeasureUnit.Commands.DeleteMeasureUnit;
public record DeleteMeasureUnitCommand(int Id) : IRequest;
public class DeleteMeasureUnitCommanddHandler : IRequestHandler<DeleteMeasureUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMeasureUnitCommanddHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMeasureUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MeasureUnits
                .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        _context.MeasureUnits.Remove(entity);

        entity.AddDomainEvent(new MeasureUnitDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

