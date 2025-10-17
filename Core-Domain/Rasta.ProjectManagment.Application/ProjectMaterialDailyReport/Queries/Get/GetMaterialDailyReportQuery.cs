using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Queries.Get
{
    public class GetMaterialDailyReportQuery : IRequest<PaginatedList<ProjectMaterialsDailyReport>>
    {
        public int ProjectId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetMaterialDailyReportQueryHandler : IRequestHandler<GetMaterialDailyReportQuery,
        PaginatedList<ProjectMaterialsDailyReport>>
    {
        private readonly IApplicationDbContext _context;
        public GetMaterialDailyReportQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<ProjectMaterialsDailyReport>> Handle(GetMaterialDailyReportQuery request
            , CancellationToken cancellationToken)
        {
            var query = _context
                              .ProjectMaterialsDailyReports
                              .Include(x => x.Unit)
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

            return await query.Select(x => new ProjectMaterialsDailyReport()
            {
                ImportAmount = x.ImportAmount,
                MaterialsDescription = x.MaterialsDescription,
                ProjectId = x.ProjectId,
                UnitId = x.UnitId,
                UnitTitle = x.Unit.Title,
                UsePlace = x.UsePlace,
                Id= x.Id
            }).PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
