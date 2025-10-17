using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Queries.Get
{
    public class GetProjectRepairDailyReportQuery: IRequest<PaginatedList<ProjectRepairDailyReport>>
    {
        public required int ProjectId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set;}
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;

    }

    public class GetProjectRepairDailyReportQueryHandler: 
            IRequestHandler<GetProjectRepairDailyReportQuery, PaginatedList<ProjectRepairDailyReport>>
    {
        private readonly IApplicationDbContext _context;
        public GetProjectRepairDailyReportQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ProjectRepairDailyReport>> Handle(GetProjectRepairDailyReportQuery request
                , CancellationToken cancellationToken)
        {
            var query = _context
                              .ProjectRepairDailyReports
                              .Where(x => x.ProjectId == request.ProjectId)
                              .AsQueryable();

            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.Created >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                request.ToDate = request.ToDate.Value.AddDays(1);
                query = query.Where(x => x.Created <= request.ToDate.Value);
            }

            return await query.PaginatedListAsync(request.PageNumber, request.PageSize); 
        }
    }
}
