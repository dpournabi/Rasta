using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Commands.CreateProjectWorkBreakdownHistoryWork;
public class CreateProjectWorkBreakdownHistoryWorkCommand : IRequest<long>
{
    public long ProjectWeekId { get; set; }
    public int ProjectWorkBreakdownId { get; set; }
    public double? PlanPercentage { get; set; }
    public double? RealPercentage { get; set; }
    public string? Description { get; set; }
    public bool IsDone { get; set; }

    public static implicit operator Domain.Entities.ProjectWorkBreakdownHistoryWork(CreateProjectWorkBreakdownHistoryWorkCommand create)
    {
        return new Domain.Entities.ProjectWorkBreakdownHistoryWork
        {
            ProjectWeekId = create.ProjectWeekId,
            ProjectWorkBreakdownId = create.ProjectWorkBreakdownId,
            PlanPercentage = 0,
            RealPercentage = create.RealPercentage,
            Description = create.Description,
            IsDone = create.IsDone
        };
    }
}
public class CreateProjectWorkBreakdownHistoryWorkCommandCommandHandler : IRequestHandler<CreateProjectWorkBreakdownHistoryWorkCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectWorkBreakdownHistoryWorkCommandCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateProjectWorkBreakdownHistoryWorkCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.ProjectWorkBreakdownHistoryWork)request;
        _context.ProjectWorkBreakdownHistoryWorks.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

