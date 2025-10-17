using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReportMonthly
{
    public class GetCPIReportMonthlyQuery : IRequest<Result<List<CPIReportMonthlyVM>>>
    {
        public long ProjectId { get; set; }
    }

    public class CPIReportWeeklyQueryHandler: IRequestHandler<GetCPIReportMonthlyQuery, Result<List<CPIReportMonthlyVM>>>
    {
        private readonly IApplicationDbContext _context;

        public CPIReportWeeklyQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<CPIReportMonthlyVM>>> Handle(GetCPIReportMonthlyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProjectWeekes
                                    .Include(x => x.ProjectWorkBreakdownHistoryWorks)
                                    .ThenInclude(x => x.ProjectWorkBreakdown)
                                .Where(x => x.ProjectId == request.ProjectId &&
                                            !x.IsDelete &&
                                            x.ProjectWorkBreakdownHistoryWorks.Any(x => x.ProjectWorkBreakdown != null && x.ProjectWorkBreakdown.IsLastNode)
                                      )
                                .Select(x => new CPIReportMonthlyVM
                                {
                                    ProjectId=x.ProjectId,
                                    MonthNumber = x.MonthNumber,
                                    CPI = x.ProjectWorkBreakdownHistoryWorks.Where(x=>x.ProjectWorkBreakdown!=null).Sum(x=> x.ProjectWorkBreakdown.CPI)
                                }).AsQueryable();
            return Result<List<CPIReportMonthlyVM>>.Success(string.Empty, await query.ToListAsync(cancellationToken));
        }
    }
}
