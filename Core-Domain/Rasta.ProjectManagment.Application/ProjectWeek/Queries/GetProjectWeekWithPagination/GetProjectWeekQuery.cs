using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetProjectWeekWithPagination;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistory.Queries.GetProjectWorkBreakdownHistoryWithPagination;
public class GetProjectWeekQuery : IRequest<PaginatedList<ProjectWeekVM>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetProjectWeekCommandHandler : IRequestHandler<GetProjectWeekQuery, PaginatedList<ProjectWeekVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectWeekCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectWeekVM>> Handle(GetProjectWeekQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<ProjectWeekVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.ProjectWeek> BuildQuery(GetProjectWeekQuery request)
    {
        var query = _context.ProjectWeekes
                            .Where(x=>!x.IsDelete)
                            .AsQueryable();
        return query;
    }
}
