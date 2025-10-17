using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.Property.Queries.GetPropertyWithPagination;
public class GetPropertyWithPaginationQuery : IRequest<PaginatedList<PropertyVM>>
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPropertyWithPaginationQueryHandler : IRequestHandler<GetPropertyWithPaginationQuery, PaginatedList<PropertyVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertyWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PropertyVM>> Handle(GetPropertyWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<PropertyVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.Property> BuildQuery(GetPropertyWithPaginationQuery request)
    {
        var query = _context.Properties.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(x => x.Name.Contains(request.Name));

        return query;
    }
}
