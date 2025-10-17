using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.MeasureUnit;

namespace Rasta.ProjectManagment.Application.MeasureUnit.Commands.UpdateMeasureUnit;
public class UpdateMeasureUnitCommand : IRequest
{
    public required int Id { get; set; }
    public required string TitleEn { get; set; }
    public required string Title { get; set; }
    public required bool IsDefault { get; set; }
}

public class UpdateMeasureUnitCommandHandler : IRequestHandler<UpdateMeasureUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMeasureUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMeasureUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MeasureUnits.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.TitleEn = request.TitleEn;
        entity.Title = request.Title;
        entity.IsDefault = request.IsDefault;

        entity.AddDomainEvent(new MeasureUnitUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
