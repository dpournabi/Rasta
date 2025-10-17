using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Commands.CreateProjectWorkBreakdown;
public class CreateProjectWorkBreakdownCommand : IRequest<long>
{
    public required int ProjectId { get; set; }
    public int? WorkBreakdownStructureId { get; set; }
    public required string WorkBreakdownStructureCode { get; set; }
    public required string Title { get; set; }
    public required bool IsCritical { get; set; }
    public required int EstimatedTime { get; set; }

    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public required DateTime LastStartDate { get; set; }
    public required DateTime LastEndDate { get; set; }

    public decimal? BaselineCost { get; set; }
    public string? Predecessors { get; set; }
    public string? Successors { get; set; }
    public string? Description { get; set; }

    public static implicit operator Domain.Entities.ProjectWorkBreakdown(CreateProjectWorkBreakdownCommand create)
    {
        return new Domain.Entities.ProjectWorkBreakdown
        {
            ProjectId = create.ProjectId,
            Title = create.Title,
            WorkBreakdownStructureId = create.WorkBreakdownStructureId,
            WorkBreakdownStructureCode = create.WorkBreakdownStructureCode,
            IsCritical = create.IsCritical,
            StartDate = create.StartDate,
            EndDate = create.EndDate,
            LastStartDate = create.LastStartDate,
            LastEndDate = create.LastEndDate,
            BaselineCost = create.BaselineCost,
            Description = create.Description,
            IsDelete = false,
            Successors = create.Successors,
            Predecessors = create.Predecessors
        };
    }
}
public class CreateProjectWorkBreakdownCommandHandler : IRequestHandler<CreateProjectWorkBreakdownCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectWorkBreakdownCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateProjectWorkBreakdownCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.ProjectWorkBreakdown)request;
        entity.AddDomainEvent(new ProjectWorkBreakdownCreatedEvent(entity));
        _context.ProjectWorkBreakdowns.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

