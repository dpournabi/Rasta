using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectTypeRatio;

namespace Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.CreateProjectTypeRatio;
public class CreateProjectTypeRatioCommand : IRequest<int>
{
    public required int ProjectTypeId { get; set; }
    public required int WFTPercentage { get; set; }
    public required int WFBPercentage { get; set; }
    public required DateTime EffectiveDate { get; set; }


    public static implicit operator Domain.Entities.ProjectTypeRatio(CreateProjectTypeRatioCommand create)
    {
        return new Domain.Entities.ProjectTypeRatio
        {
            ProjectTypeId = create.ProjectTypeId,
            WFTPercentage = create.WFTPercentage,
            WFBPercentage = create.WFBPercentage,
            EffectiveDate = create.EffectiveDate
        };
    }
}
public class CreateProjectTypeRatioCommandHandler : IRequestHandler<CreateProjectTypeRatioCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectTypeRatioCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectTypeRatioCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.ProjectTypeRatio)request;
        entity.AddDomainEvent(new ProjectTypeRatioCreatedEvent(entity));
        _context.ProjectTypeRatios.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
