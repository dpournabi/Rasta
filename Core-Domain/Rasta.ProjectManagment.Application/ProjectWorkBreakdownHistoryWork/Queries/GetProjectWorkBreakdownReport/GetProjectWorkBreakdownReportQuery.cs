using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Mappings;
using System.ComponentModel;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Queries.GetProjectWorkBreakdownReport;
public class GetProjectWorkBreakdownReportQuery : IRequest<PaginatedList<ProjectWorkBreakdownReportVM>>
{
    public required int ProjectId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    /// <summary>
    /// Project managment report
    /// <example>Sample1: Range From-TO 1..5</example>
    /// <example>Sample2: Ranges separate by comma like 1,5,9</example>
    /// </summary>
    [Description("Range format 1..5 and comma format 1,5,9")]
    public string? WeekNumberRange { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? WeekNumber { get; set; }
    public int? MonthNumber { get; set; }
    public bool OrderByDesc { get; set; }
    public bool ShowAll { get; set; }
}

public class GetProjectWorkBreakdownReportCommandHandler : IRequestHandler<GetProjectWorkBreakdownReportQuery, PaginatedList<ProjectWorkBreakdownReportVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectWorkBreakdownReportCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<PaginatedList<ProjectWorkBreakdownReportVM>> Handle(GetProjectWorkBreakdownReportQuery request, CancellationToken cancellationToken)
    {

        (IEnumerable<int> weekCounts, bool isRange, int from, int to)? _extractWeekCount = null;

        var query = (from projectWeek in _context.ProjectWeekes.Include(x => x.Project)
                     join projectWorkBreakdownHistoryWork in _context.ProjectWorkBreakdownHistoryWorks on projectWeek.Id equals projectWorkBreakdownHistoryWork.ProjectWeekId
                     join pwb in _context.ProjectWorkBreakdowns on projectWorkBreakdownHistoryWork.ProjectWorkBreakdownId equals pwb.Id into ppwb
                     from projectWorkBreakdown in ppwb.DefaultIfEmpty()
                     //join workBreakdownStructure in _context.WorkBreakdownStructures on projectWorkBreakdown.WorkBreakdownStructureId equals workBreakdownStructure.Id
                     select new { projectWeek, projectWorkBreakdownHistoryWork, projectWorkBreakdown })
                     .Where(x => x.projectWeek.ProjectId == request.ProjectId &&
                                 x.projectWorkBreakdown.IsLastNode &&
                                 !x.projectWorkBreakdown.IsDelete &&
                                (request.StartDate == null || x.projectWeek.StartDate >= request.StartDate.Value) &&
                                (request.EndDate == null || x.projectWeek.EndDate <= request.EndDate.Value) &&
                                (request.WeekNumber == null || x.projectWeek.WeekCount == request.WeekNumber) &&
                                (request.MonthNumber == null || x.projectWeek.MonthNumber == request.MonthNumber)
                                )
                     .AsQueryable();

        if (request.WeekNumberRange != null)
        {
            _extractWeekCount = await ExtractWeekCounts(request.WeekNumberRange).ConfigureAwait(false);
            if (_extractWeekCount.Value.isRange)
            {
                query = query.Where(x => x.projectWeek.WeekCount >= _extractWeekCount.Value.from && x.projectWeek.WeekCount <= _extractWeekCount.Value.to);
            }
            else
            {
                query = query.Where(x => _extractWeekCount.Value.weekCounts.Contains(x.projectWeek.WeekCount));
            }
        }

        if (request.ShowAll)
        {
            request.PageSize = query.Count();
            request.PageNumber = 1;
        }
        else
        {
            var _currentDate = DateTime.Now;
            var defaultWeek = await _context.ProjectWeekes.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && (_currentDate >= x.StartDate && _currentDate <= x.EndDate));
            if (defaultWeek != null)
                query = query.Where(x => x.projectWeek.WeekCount == defaultWeek.WeekCount);
        }

        if (request.OrderByDesc)
            query = query.OrderByDescending(x => x.projectWeek.WeekCount);

        var result = query.Select(x => new ProjectWorkBreakdownReportVM
        {
            WBSCode = x.projectWorkBreakdown.WorkBreakdownStructureCode,
            Title = x.projectWorkBreakdown.Title,
            StartDate = x.projectWeek.StartDate,
            EndDate = x.projectWeek.EndDate,
            IsCretical = x.projectWorkBreakdown.IsCritical,
            PE = x.projectWorkBreakdownHistoryWork.PlanPercentage.Value,
            PlanCompleteProgressPercentage = x.projectWorkBreakdownHistoryWork.PlanCompleteProgressPercentage,
            PlanCumulativePercentage = x.projectWorkBreakdownHistoryWork.PlanCumulativePercentage, 
            PlanWeightProgressPercentage = x.projectWorkBreakdownHistoryWork.PlanWeightProgressPercentage,

            AC = x.projectWorkBreakdownHistoryWork.RealPercentage,
            ActualCompleteProgressPercentage = x.projectWorkBreakdownHistoryWork.ActualCompleteProgressPercentage,
            ActualWeightProgressPercentage = x.projectWorkBreakdownHistoryWork.ActualWeightProgressPercentage,
            RealCumulativePercentage = x.projectWorkBreakdownHistoryWork.RealCumulativePercentage,
            WeekNumber = x.projectWeek.WeekCount.ToString(),
            ACB = x.projectWorkBreakdownHistoryWork.ACB,
            CV = x.projectWorkBreakdownHistoryWork.CV,
            EV = x.projectWorkBreakdownHistoryWork.EV,
            PV = x.projectWorkBreakdownHistoryWork.PV,
            SV = x.projectWorkBreakdownHistoryWork.SV
        });

        if (request.WeekNumberRange != null)
        {
            return await result.GroupBy(o => o.WBSCode).Select(o => new ProjectWorkBreakdownReportVM
            {
                WBSCode = string.Join(',', o.Select(x => x.WBSCode).ToList()),
                Title = string.Join(',', o.Select(x => x.Title).ToList()),
                StartDate = o.First().StartDate,
                EndDate = o.OrderByDescending(x => x.EndDate).First().EndDate,
                IsCretical = null,
                PE = o.Sum(xx => xx.PE),
                PlanCompleteProgressPercentage = o.Sum(xx => xx.PlanCompleteProgressPercentage),
                PlanCumulativePercentage = o.Sum(xx => xx.PlanCumulativePercentage),
                PlanWeightProgressPercentage = o.Sum(xx => xx.PlanWeightProgressPercentage),
                AC = o.Sum(xx => xx.AC),
                ActualCompleteProgressPercentage = o.Sum(xx => xx.ActualCompleteProgressPercentage),
                ActualWeightProgressPercentage = o.Sum(xx => xx.ActualWeightProgressPercentage),
                RealCumulativePercentage = o.Sum(xx => xx.RealCumulativePercentage),
                WeekNumber = string.Join(',', o.Select(x => x.WeekNumber).ToList()),
                ACB = o.Sum(xx => xx.ACB),
                CV = o.Sum(xx => xx.CV),
                EV = o.Sum(xx => xx.EV),
                PV = o.Sum(xx => xx.PV),
                SV = o.Sum(xx => xx.SV)
            }).PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return await result.PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private async Task<(IEnumerable<int> weekCounts, bool isRange, int from, int to)> ExtractWeekCounts(string weekCounts)
    {
        (IEnumerable<int> weekCounts, bool isRange, int from, int to) result = new() { isRange = true };

        if (weekCounts.Contains(".."))
        {
            result.weekCounts = weekCounts.Split("..").Distinct().Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToList();
            result.from = result.weekCounts.First();
            result.to = result.weekCounts.OrderByDescending(x=>x).First();
            return await Task.FromResult(result);
        }

        result.isRange = false;
        result.weekCounts = weekCounts.Split(',').Distinct().Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToList();
        return await Task.FromResult(result);
    }
}
