using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReport.GetCPIReportWeekly
{
    public class GetCPIReportWeeklyQuery : IRequest<Result<List<CPIReportWeeklyVM>>>
    {
        public long ProjectId { get; set; }
    }

    public class CPIReportWeeklyQueryHandler: IRequestHandler<GetCPIReportWeeklyQuery, Result<List<CPIReportWeeklyVM>>>
    {
        private readonly IApplicationDbContext _context;

        public CPIReportWeeklyQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<CPIReportWeeklyVM>>> Handle(GetCPIReportWeeklyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProjectWeekes
                                    .Include(x => x.ProjectWorkBreakdownHistoryWorks)
                                    .ThenInclude(x => x.ProjectWorkBreakdown)
                                .Where(x => x.ProjectId == request.ProjectId &&
                                            !x.IsDelete &&
                                            x.ProjectWorkBreakdownHistoryWorks.Any(x => x.ProjectWorkBreakdown != null && x.ProjectWorkBreakdown.IsLastNode)
                                      )
                                .Select(x => new CPIReportWeeklyVM
                                {
                                    ProjectId=x.ProjectId,
                                    WeekCount = x.WeekCount,
                                    CPI = x.ProjectWorkBreakdownHistoryWorks.Where(x=>x.ProjectWorkBreakdown!=null).Sum(x=> x.ProjectWorkBreakdown.CPI)
                                }).AsQueryable();
            return Result<List<CPIReportWeeklyVM>>.Success(string.Empty, await query.ToListAsync(cancellationToken));
        }
    }
}
