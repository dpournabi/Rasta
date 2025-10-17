using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.ProjectTypeRatio.Queries.GetProjectTypeRatioWithPagination;
public class GetProjectTypeRatioQuery : IRequest<PaginatedList<ProjectTypeRatioVM>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProjectTypeRatioQueryHandler : IRequestHandler<GetProjectTypeRatioQuery, PaginatedList<ProjectTypeRatioVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectTypeRatioQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectTypeRatioVM>> Handle(GetProjectTypeRatioQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<ProjectTypeRatioVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.ProjectTypeRatio> BuildQuery(GetProjectTypeRatioQuery request)
    {
        var query = _context.ProjectTypeRatios.AsQueryable();
        return query;
    }
}
