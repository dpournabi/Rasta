using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.BasicInformation.Queries.GetBasicInformationWithPagination;
public class GetBasicInformationQuery : IRequest<BasicInformationVM>
{
}
public class GetBasicInformationQueryHandler : IRequestHandler<GetBasicInformationQuery, BasicInformationVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBasicInformationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BasicInformationVM> Handle(GetBasicInformationQuery request, CancellationToken cancellationToken)
    {
        var firstNode = await _context.BasicInformations.Select(f => new BasicInformationVM()
        {
            Code = f.Code,
            Name = f.Name,
            MeasureUnitId = f.MeasureUnitId,
            Id = f.Id,
            ParentId = f.ParentId,
            Level = f.Level
        }).FirstOrDefaultAsync(f => f.ParentId == null);
        return await CreateTree(firstNode);
    }

    #region Private Methods

    private async Task<BasicInformationVM> CreateTree(BasicInformationVM node)
    {
        var childs = _context.BasicInformations.Select(f => new BasicInformationVM()
        {
            Code = f.Code,
            Id = f.Id,
            ParentId = f.ParentId,
            Name = f.Name,
            MeasureUnitId = f.MeasureUnitId,
            Level = f.Level
        }).Where(f => f.ParentId == node.Id);

        foreach (var item in childs)
        {
            node.Childrens.Add(item);
            await CreateTree(item);
        }

        return node;
    }
    #endregion




}
