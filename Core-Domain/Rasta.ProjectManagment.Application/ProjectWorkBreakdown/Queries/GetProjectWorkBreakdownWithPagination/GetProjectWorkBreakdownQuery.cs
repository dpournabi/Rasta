using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetProjectWorkBreakdownWithPagination;
public class GetProjectWorkBreakdownQuery : IRequest<PaginatedList<ProjectWorkBreakdownVM>>
{
    public required long ProjectId { get; init; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool ShowAll { get; init; }
}
public class GetProjectWorkBreakdownCommandHandler : IRequestHandler<GetProjectWorkBreakdownQuery, PaginatedList<ProjectWorkBreakdownVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectWorkBreakdownCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectWorkBreakdownVM>> Handle(GetProjectWorkBreakdownQuery request, CancellationToken cancellationToken)
    {
        var query = BuildQuery(request);

        if (request.ShowAll)
        {
            request.PageNumber = 1;
            request.PageSize = query.Count();
        }

        return await query.ProjectTo<ProjectWorkBreakdownVM>(_mapper.ConfigurationProvider)
                          .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.ProjectWorkBreakdown> BuildQuery(GetProjectWorkBreakdownQuery request)
    {
        var query = _context.ProjectWorkBreakdowns
                            .Where(x=>x.ProjectId == request.ProjectId && !x.IsDelete)
                            .AsQueryable();
        return query;
    }
}
