using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Queries.GetProjectWorkBreakdownProgressReport;
public class GetProjectWorkBreakdownProgressReportQuery : IRequest<List<ProjectWorkBreakdownProgressReportVM>>
{
    public int ProjectId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? WeekNumber { get; set; }
    public int? MonthNumber { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool ShowAll { get; set; }
    public bool OrderByDesc { get; set; }
}

public class GetProjectWorkBreakdownProgressReportCommandHandler : IRequestHandler<GetProjectWorkBreakdownProgressReportQuery, List<ProjectWorkBreakdownProgressReportVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectWorkBreakdownProgressReportCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProjectWorkBreakdownProgressReportVM>> Handle(GetProjectWorkBreakdownProgressReportQuery request, CancellationToken cancellationToken)
    {
        var projectWeeks = _context.ProjectWeekes
                  .Where(x => x.ProjectId == request.ProjectId &&
                              !x.IsDelete &&
                              (request.StartDate == null || x.StartDate >= request.StartDate.Value) &&
                              (request.EndDate == null || x.EndDate <= request.EndDate.Value) &&
                              (request.WeekNumber == null || x.WeekCount == request.WeekNumber.Value) &&
                               (request.MonthNumber == null || x.MonthNumber == request.MonthNumber.Value)
                              ).AsQueryable();

        if (request.OrderByDesc)
            projectWeeks = projectWeeks.OrderByDescending(x => x.WeekCount);
        else
            projectWeeks = projectWeeks.OrderBy(x => x.WeekCount);

        var projectWeekIds = projectWeeks.Select(x => x.Id).ToList();
        var ProjectWorkBreakdownHistoryWorks = _context.ProjectWorkBreakdownHistoryWorks
                                                             .Include(x => x.ProjectWeek)
                                                             .Where(x => projectWeekIds.Contains(x.ProjectWeekId))
                                                             .AsQueryable();

        var query = (from pw in projectWeeks

                     select new ProjectWorkBreakdownProgressReportVM
                     {
                         WeekCount = pw.WeekCount,
                         StartDate = pw.StartDate,
                         CumulativePlanCompleteProgressPercentage = ProjectWorkBreakdownHistoryWorks.Where(c => c.ProjectWeek.WeekCount <= pw.WeekCount).Sum(x => x.PlanCompleteProgressPercentage),
                         CumulativeActualCompleteProgressPercentage = ProjectWorkBreakdownHistoryWorks.Where(c => c.ProjectWeek.WeekCount <= pw.WeekCount).Sum(x => x.ActualCompleteProgressPercentage),
                     }).AsQueryable();

        var result = await query.ToListAsync();

        return result;
    }
}
