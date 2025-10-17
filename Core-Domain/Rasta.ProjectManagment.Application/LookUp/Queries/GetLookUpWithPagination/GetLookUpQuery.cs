using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.LookUp.Queries.GetLookUpWithPagination;
public class GetLookUpQuery : IRequest<PaginatedList<LookUpVM>>
{
    public string Type { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetLookUpQueryHandler : IRequestHandler<GetLookUpQuery, PaginatedList<LookUpVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLookUpQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LookUpVM>> Handle(GetLookUpQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<LookUpVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.LookUp> BuildQuery(GetLookUpQuery request)
    {
        var query = _context.LookUps.AsQueryable();

        query = query.Where(x => request.Type==null || x.Type.Equals(request.Type));

        return query;
    }
}
