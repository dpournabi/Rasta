using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;

namespace Rasta.ProjectManagment.Application.MeasureUnit.Queries.GetMeasureUnitWithPagination;
public class GetMeasureUnitQuery : IRequest<PaginatedList<MeasureUnitVM>>
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? TitleEn { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetMeasureUnitQueryHandler : IRequestHandler<GetMeasureUnitQuery, PaginatedList<MeasureUnitVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMeasureUnitQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MeasureUnitVM>> Handle(GetMeasureUnitQuery request, CancellationToken cancellationToken)
    {
        return await BuildQuery(request)
            .ProjectTo<MeasureUnitVM>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private IQueryable<Domain.Entities.MeasureUnit> BuildQuery(GetMeasureUnitQuery request)
    {
        var query = _context.MeasureUnits.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
            query = query.Where(x => x.Title.Contains(request.Title));

        if (!string.IsNullOrWhiteSpace(request.TitleEn))
            query = query.Where(x => x.TitleEn.Contains(request.TitleEn));

        if (request.Id.HasValue)
        {
            query = query.Where(x => x.Id == request.Id);
        }

        return query;
    }
}
