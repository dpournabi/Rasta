using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReport.GetSPIReportWeekly
{
    public class GetSPIReportWeeklyQuery : IRequest<Result<List<SPIReportWeeklyVM>>>
    {
        public long ProjectId { get; set; }
    }

    public class GetSPIReportWeeklyQueryHandler : IRequestHandler<GetSPIReportWeeklyQuery, Result<List<SPIReportWeeklyVM>>>
    {
        private readonly IApplicationDbContext _context;

        public GetSPIReportWeeklyQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<SPIReportWeeklyVM>>> Handle(GetSPIReportWeeklyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProjectWeekes
                                    .Include(x => x.ProjectWorkBreakdownHistoryWorks)
                                    .ThenInclude(x => x.ProjectWorkBreakdown)
                                .Where(x => x.ProjectId == request.ProjectId &&
                                            !x.IsDelete &&
                                            x.ProjectWorkBreakdownHistoryWorks.Any(x => x.ProjectWorkBreakdown != null && x.ProjectWorkBreakdown.IsLastNode)
                                      )
                                .Select(x => new SPIReportWeeklyVM
                                {
                                    ProjectId=x.ProjectId,
                                    WeekCount = x.WeekCount,
                                    SPI = x.ProjectWorkBreakdownHistoryWorks.Where(x=>x.ProjectWorkBreakdown!=null).Sum(x=> x.ProjectWorkBreakdown.SPI)
                                }).AsQueryable();
            return Result<List<SPIReportWeeklyVM>>.Success(string.Empty, await query.ToListAsync(cancellationToken));
        }
    }
}
