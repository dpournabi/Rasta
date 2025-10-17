using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Queries.Get
{
    public class GetProjectAccidentDailyReportQuery : IRequest<PaginatedList<ProjectAccidentDailyReportBriefVM>>
    {
        public required int ProjectId { get; set; }
        public string? Reason { get; set; }
        public int? AccidentTypeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetProjectAccidentDailyReportQueryHandler : IRequestHandler<GetProjectAccidentDailyReportQuery, PaginatedList<ProjectAccidentDailyReportBriefVM>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectAccidentDailyReportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProjectAccidentDailyReportBriefVM>> Handle(GetProjectAccidentDailyReportQuery request, CancellationToken cancellationToken)
        {
            return await BuildQuery(request)
                .ProjectTo<ProjectAccidentDailyReportBriefVM>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private IQueryable<Domain.Entities.ProjectAccidentDailyReport> BuildQuery(GetProjectAccidentDailyReportQuery request)
        {
            var query = _context
                              .ProjectAccidentDailyReports
                              .Include(x => x.AccidentType)
                              .Where(x => x.ProjectId == request.ProjectId)
                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Reason))
                query = query.Where(x => x.Reason != null && x.Reason.Contains(request.Reason));

            if (request.AccidentTypeId!=null && request.AccidentTypeId.HasValue)
                query = query.Where(x => x.AccidentTypeId == request.AccidentTypeId.Value);

            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.Created >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                request.ToDate = request.ToDate.Value.AddDays(1);
                query = query.Where(x => x.Created <= request.ToDate.Value);
            }

            return query;
        }
    }
}