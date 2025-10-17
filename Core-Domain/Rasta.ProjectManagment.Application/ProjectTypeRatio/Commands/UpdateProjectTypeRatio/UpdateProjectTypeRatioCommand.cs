using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectTypeRatio;

namespace Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.UpdateProjectTypeRatio;
public class UpdateProjectTypeRatioCommand : IRequest
{
    public required int Id { get; set; }
    public required int ProjectTypeId { get; set; }
    public required int WFTPercentage { get; set; }
    public required int WFBPercentage { get; set; }
    public required DateTime EffectiveDate { get; set; }
}
public class UpdateProjectTypeRatioCommandHandler : IRequestHandler<UpdateProjectTypeRatioCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectTypeRatioCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProjectTypeRatioCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectTypeRatios.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.ProjectTypeId = request.ProjectTypeId;
        entity.WFTPercentage = request.WFTPercentage;
        entity.WFBPercentage = request.WFBPercentage;
        entity.EffectiveDate = request.EffectiveDate;


        entity.AddDomainEvent(new ProjectTypeRatioUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
