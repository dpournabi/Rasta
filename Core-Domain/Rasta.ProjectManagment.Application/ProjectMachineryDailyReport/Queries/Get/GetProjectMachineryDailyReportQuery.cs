using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Queries.Get
{
    public record GetProjectMachineryDailyReportQuery : IRequest<PaginatedList<ProjectMachineryDailyReportBriefVM>>
    {
        public required int ProjectId { get; set; }
        public required string MachineryEquipmentDescription { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetProjectMachineryDailyReportQueryHandler : IRequestHandler<GetProjectMachineryDailyReportQuery, PaginatedList<ProjectMachineryDailyReportBriefVM>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectMachineryDailyReportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProjectMachineryDailyReportBriefVM>> Handle(GetProjectMachineryDailyReportQuery request, CancellationToken cancellationToken)
        {
            return await BuildQuery(request)
                .ProjectTo<ProjectMachineryDailyReportBriefVM>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private IQueryable<Domain.Entities.ProjectMachineryDailyReport> BuildQuery(GetProjectMachineryDailyReportQuery request)
        {
            var query = _context
                              .ProjectMachineryDailyReports
                              .Where(x => x.ProjectId == request.ProjectId)
                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.MachineryEquipmentDescription))
                query = query.Where(x => x.MachineryEquipmentDescription != null && x.MachineryEquipmentDescription.Contains(request.MachineryEquipmentDescription));

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