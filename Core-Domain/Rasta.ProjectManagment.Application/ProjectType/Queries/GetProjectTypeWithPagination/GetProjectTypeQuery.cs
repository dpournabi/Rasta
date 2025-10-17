using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectType.Queries.GetProjectTypeWithPagination;
public class GetProjectTypeQuery : IRequest<PaginatedList<ProjectTypeVM>>
{
    public string? Title { get; set; }
    public string? TitleEn { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProjectTypeQueryHandler : IRequestHandler<GetProjectTypeQuery, PaginatedList<ProjectTypeVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectTypeVM>> Handle(GetProjectTypeQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<ProjectTypeVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.ProjectType> BuildQuery(GetProjectTypeQuery request)
    {
        var query = _context.projectTypes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
            query = query.Where(x => x.Title.Contains(request.Title));

        if (!string.IsNullOrWhiteSpace(request.TitleEn))
            query = query.Where(x => x.TitleEn.Contains(request.TitleEn));

        return query;
    }
}
