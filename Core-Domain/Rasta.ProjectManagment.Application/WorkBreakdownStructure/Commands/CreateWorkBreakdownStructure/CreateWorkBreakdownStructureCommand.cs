//using MediatR;
//using Rasta.ProjectManagment.Application.Common.Interfaces;
//using Rasta.ProjectManagment.Domain.Events;
//using Rasta.ProjectManagment.Domain.Events.WorkBreakdownStructure;

//namespace Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.CreateWorkBreakdownStructure;
//public class CreateWorkBreakdownStructureCommand : IRequest<int>
//{
//    public required string Code { get; set; }
//    public required string BudjetCode { get; set; }
//    public required string TitleEn { get; set; }
//    public required string Title { get; set; }
//    public int? ParentId { get; set; }

//    public static implicit operator Domain.Entities.WorkBreakdownStructure(CreateWorkBreakdownStructureCommand command)
//    {
//        return new Domain.Entities.WorkBreakdownStructure()
//        {
//            Code = command.Code,
//            BudjetCode = command.BudjetCode,
//            TitleEn = command.TitleEn,
//            Title = command.Title,
//            ParentId = command.ParentId
//        };
//    }
//}
//public class CreateWorkBreakdownStructureCommandHandler : IRequestHandler<CreateWorkBreakdownStructureCommand, int>
//{
//    private readonly IApplicationDbContext _context;

//    public CreateWorkBreakdownStructureCommandHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }
//    public async Task<int> Handle(CreateWorkBreakdownStructureCommand request, CancellationToken cancellationToken)
//    {
//        var entity = (Domain.Entities.WorkBreakdownStructure)request;
//        entity.AddDomainEvent(new WorkBreakdownStructureCreatedEvent(entity));
//        _context.WorkBreakdownStructures.Add(entity);
//        await _context.SaveChangesAsync(cancellationToken);
//        return entity.Id;
//    }
//}

