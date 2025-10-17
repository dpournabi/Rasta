using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Queries.Get
{
    public class GetProjectGuestDailyReportQuery : IRequest<PaginatedList<ProjectGuestDailyReportBriefVM>>
    {
        public required int ProjectId { get; set; }
        public required string VisitorName { get; set; }
        public string? OrganizationName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetProjectAccidentDailyReportQueryHandler : IRequestHandler<GetProjectGuestDailyReportQuery, PaginatedList<ProjectGuestDailyReportBriefVM>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectAccidentDailyReportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProjectGuestDailyReportBriefVM>> Handle(GetProjectGuestDailyReportQuery request, CancellationToken cancellationToken)
        {
            return await BuildQuery(request)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private IQueryable<ProjectGuestDailyReportBriefVM> BuildQuery(GetProjectGuestDailyReportQuery request)
        {
            var query = _context
                              .ProjectGuestDailyReports
                              .Where(x => x.ProjectId == request.ProjectId)
                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.VisitorName))
                query = query.Where(x => x.VisitorName != null && x.VisitorName.Contains(request.VisitorName));

            if (!string.IsNullOrWhiteSpace(request.OrganizationName))
                query = query.Where(x => x.OrganizationName != null && x.OrganizationName.Contains(request.OrganizationName));

            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.Created >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                request.ToDate = request.ToDate.Value.AddDays(1);
                query = query.Where(x => x.Created <= request.ToDate.Value);
            }

            return query.Select(x => new ProjectGuestDailyReportBriefVM()
            {
                EnterTime = x.EnterTime,
                ExitTime = x.ExitTime,
                Id = x.Id,
                ProjectId = x.ProjectId,
                VisitorName = x.VisitorName,
                OrganizationName = x.OrganizationName,
            });
        }
    }
}