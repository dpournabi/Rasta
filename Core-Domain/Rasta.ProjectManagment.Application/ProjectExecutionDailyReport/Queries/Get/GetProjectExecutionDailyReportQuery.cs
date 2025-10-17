using MediatR;
using AutoMapper;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get;

public class GetProjectExecutionDailyReportQuery : IRequest<PaginatedList<ProjectExecutionDailyReportBriefVM>>
{
    public required int ProjectId { get; set; }
    public string? ZoneNo { get; set; }
    public string? BlockNo { get; set; }
    public string? MainOperation { get; set; }
    public string? SubOperation { get; set; }
    public string? Location { get; set; }
    public string? SubContractorName { get; set; }
    public string? Persons { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetProjectExecutionDailyReportQueryHandler : IRequestHandler<GetProjectExecutionDailyReportQuery, PaginatedList<ProjectExecutionDailyReportBriefVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectExecutionDailyReportQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectExecutionDailyReportBriefVM>> Handle(GetProjectExecutionDailyReportQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<ProjectExecutionDailyReportBriefVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.ProjectExecutionDailyReport> BuildQuery(GetProjectExecutionDailyReportQuery request)
    {
        var query = _context
                          .ProjectExecutionDailyReports
                          .Where(x => x.ProjectId == request.ProjectId)
                          .AsQueryable();
                
        if (!string.IsNullOrWhiteSpace(request.SubContractorName))
            query = query.Where(x => x.SubContractorName != null && x.SubContractorName.Contains(request.SubContractorName));

        if (!string.IsNullOrWhiteSpace(request.Location))
            query = query.Where(x => x.Location != null && x.Location.Contains(request.Location));

        if (!string.IsNullOrWhiteSpace(request.Persons))
            query = query.Where(x => x.Persons != null && x.Persons.Contains(request.Persons));

        if (!string.IsNullOrWhiteSpace(request.BlockNo))
            query = query.Where(x => x.BlockNo != null && x.BlockNo.Contains(request.BlockNo));

        if (!string.IsNullOrWhiteSpace(request.ZoneNo))
            query = query.Where(x => x.ZoneNo != null && x.ZoneNo.Contains(request.ZoneNo));

        if (!string.IsNullOrWhiteSpace(request.MainOperation))
            query = query.Where(x => x.MainOperation != null && x.MainOperation.Contains(request.MainOperation));

        if (!string.IsNullOrWhiteSpace(request.SubOperation))
            query = query.Where(x => x.SubOperation != null && x.SubOperation.Contains(request.SubOperation));
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