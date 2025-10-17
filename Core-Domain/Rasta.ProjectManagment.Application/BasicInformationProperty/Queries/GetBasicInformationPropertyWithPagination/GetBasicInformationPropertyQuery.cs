using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Queries.GetBasicInformationPropertyWithPagination;
public class GetBasicInformationPropertyQuery : IRequest<PaginatedList<BasicInformationPropertyVM>>
{
    public required string Value { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetBasicInformationPropertyQueryHandler : IRequestHandler<GetBasicInformationPropertyQuery, PaginatedList<BasicInformationPropertyVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBasicInformationPropertyQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BasicInformationPropertyVM>> Handle(GetBasicInformationPropertyQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<BasicInformationPropertyVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.BasicInformationProperty> BuildQuery(GetBasicInformationPropertyQuery request)
    {
        var query = _context.BasicInformationProperties.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Value))
            query = query.Where(x => x.Value.Contains(request.Value));

        return query;
    }
}
