using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Commands.UpdateProjectWorkBreakdown;
public class UpdateProjectWorkBreakdownCommand : IRequest
{
    public required long Id { get; set; }
    public required int ProjectId { get; set; }
    public int? WorkBreakdownStructureId { get; set; }
    public required string WorkBreakdownStructureCode { get; set; }

    public required bool IsCritical { get; set; }
    public required int EstimatedTime { get; set; }

    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public required DateTime LastStartDate { get; set; }
    public required DateTime LastEndDate { get; set; }

    public decimal? BaselineCost { get; set; }
    public required string Title { get; set; }
    public string? Predecessors { get; set; }
    public string? Successors { get; set; }
    public string? Description { get; set; }
}
public class UpdateProjectWorkBreakdownHandler : IRequestHandler<UpdateProjectWorkBreakdownCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectWorkBreakdownHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProjectWorkBreakdownCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectWorkBreakdowns.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.ProjectId = request.ProjectId;
        entity.WorkBreakdownStructureId = request.WorkBreakdownStructureId;
        entity.WorkBreakdownStructureCode = request.WorkBreakdownStructureCode;
        entity.IsCritical = request.IsCritical;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LastStartDate = request.LastStartDate;
        entity.LastEndDate = request.LastEndDate;
        entity.BaselineCost = request.BaselineCost;
        entity.Description = request.Description;
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Successors = request.Successors;
        entity.Predecessors = request.Predecessors;

        entity.AddDomainEvent(new ProjectWorkBreakdownUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
