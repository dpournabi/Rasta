using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Queries.Get
{
    public class GetProjectProblemDailyReportQuery : IRequest<PaginatedList<ProjectProblemDailyReportBriefVM>>
    {
        public required int ProjectId { get; set; }
        public required string ProblemsClassifications { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetProjectAccidentDailyReportQueryHandler : IRequestHandler<GetProjectProblemDailyReportQuery, PaginatedList<ProjectProblemDailyReportBriefVM>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectAccidentDailyReportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProjectProblemDailyReportBriefVM>> Handle(GetProjectProblemDailyReportQuery request, CancellationToken cancellationToken)
        {
            return await BuildQuery(request)                
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private IQueryable<ProjectProblemDailyReportBriefVM> BuildQuery(GetProjectProblemDailyReportQuery request)
        {
            var query = _context
                              .ProjectProblemDailyReports
                              .Where(x => x.ProjectId == request.ProjectId)
                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.ProblemsClassifications))
                query = query.Where(x => x.ProblemsClassifications != null && x.ProblemsClassifications.Contains(request.ProblemsClassifications));

            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.Created >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                request.ToDate = request.ToDate.Value.AddDays(1);
                query = query.Where(x => x.Created <= request.ToDate.Value);
            }

            return query.Select(x => new ProjectProblemDailyReportBriefVM() { 
                Description = x.Description,
                Id = x.Id,
                ProblemsClassifications = x.ProblemsClassifications,
                ProjectId = x.ProjectId
            });
        }
    }
}