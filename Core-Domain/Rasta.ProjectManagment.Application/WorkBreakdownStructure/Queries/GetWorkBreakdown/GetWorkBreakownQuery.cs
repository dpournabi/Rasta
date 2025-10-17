//using AutoMapper;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Rasta.ProjectManagment.Application.Common.Interfaces;

//namespace Rasta.ProjectManagment.Application.WorkBreakdownStructure.Queries.GetWorkBreakdown;
//public class GetWorkBreakownQuery : IRequest<WorkBreakdownStructureVM>
//{
//}
//public class GetWorkBreakownQueryHandler : IRequestHandler<GetWorkBreakownQuery, WorkBreakdownStructureVM>
//{
//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public GetWorkBreakownQueryHandler(IApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<WorkBreakdownStructureVM> Handle(GetWorkBreakownQuery request, CancellationToken cancellationToken)
//    {
//        var firstNode = await _context.WorkBreakdownStructures.Select(f => new WorkBreakdownStructureVM()
//        {
//            Code = f.Code,
//            Id = f.Id,
//            ParentId = f.ParentId,
//            BudjetCode = f.Code,
//            Title = f.Title,
//            TitleEn = f.TitleEn,
//            Level = f.Level
//        }).FirstOrDefaultAsync(f => f.ParentId == null);
//        var result = await CreateTree(firstNode);
//        return result;
//    }

//    #region Private Methods

//    private async Task<WorkBreakdownStructureVM> CreateTree(WorkBreakdownStructureVM node)
//    {
//        var childs = _context.WorkBreakdownStructures.Select(f => new WorkBreakdownStructureVM()
//        {
//            Code = f.Code,
//            Id = f.Id,
//            ParentId = f.ParentId,
//            BudjetCode = f.Code,
//            Title = f.Title,
//            TitleEn = f.TitleEn,
//            Level = f.Level
//        }).Where(f => f.ParentId == node.Id);

//        foreach (var item in childs)
//        {
//            node.Childrens.Add(item);
//            await CreateTree(item);
//        }

//        return node;
//    }
//    #endregion
//}
