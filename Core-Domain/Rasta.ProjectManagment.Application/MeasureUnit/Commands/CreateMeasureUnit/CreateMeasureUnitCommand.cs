using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Entities;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.MeasureUnit;

namespace Rasta.ProjectManagment.Application.MeasureUnit.Commands.CreateMeasureUnit;
public class CreateMeasureUnitCommand : IRequest<int>
{
    public required string TitleEn { get; set; }
    public required string Title { get; set; }
    public required bool IsDefault { get; set; }
   

    public static implicit operator Domain.Entities.MeasureUnit(CreateMeasureUnitCommand create)
    {
        return new Domain.Entities.MeasureUnit
        {
            TitleEn = create.TitleEn,
            Title = create.Title,
            IsDefault = create.IsDefault
        };
    }
}
public class CreateMeasureUnitCommandHandler : IRequestHandler<CreateMeasureUnitCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMeasureUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMeasureUnitCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.MeasureUnit)request;
        entity.AddDomainEvent(new MeasureUnitCreatedEvent(entity));
        _context.MeasureUnits.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
