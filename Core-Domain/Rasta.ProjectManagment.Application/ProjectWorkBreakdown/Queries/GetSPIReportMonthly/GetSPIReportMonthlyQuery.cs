using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSPIReportMonthly
{
    public class GetSPIReportMonthlyQuery : IRequest<Result<List<SPIReportMonthlyVM>>>
    {
        public long ProjectId { get; set; }
    }

    public class CPIReportWeeklyQueryHandler: IRequestHandler<GetSPIReportMonthlyQuery, Result<List<SPIReportMonthlyVM>>>
    {
        private readonly IApplicationDbContext _context;

        public CPIReportWeeklyQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<SPIReportMonthlyVM>>> Handle(GetSPIReportMonthlyQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProjectWeekes
                                    .Include(x => x.ProjectWorkBreakdownHistoryWorks)
                                    .ThenInclude(x => x.ProjectWorkBreakdown)
                                .Where(x => x.ProjectId == request.ProjectId &&
                                            !x.IsDelete &&
                                            x.ProjectWorkBreakdownHistoryWorks.Any(x => x.ProjectWorkBreakdown != null && x.ProjectWorkBreakdown.IsLastNode)
                                      )
                                .Select(x => new SPIReportMonthlyVM
                                {
                                    ProjectId=x.ProjectId,
                                    MonthNumber = x.MonthNumber,
                                    SPI = x.ProjectWorkBreakdownHistoryWorks.Where(x=>x.ProjectWorkBreakdown!=null).Sum(x=> x.ProjectWorkBreakdown.SPI)
                                }).AsQueryable();
            return Result<List<SPIReportMonthlyVM>>.Success(string.Empty, await query.ToListAsync(cancellationToken));
        }
    }
}
