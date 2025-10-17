//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Rasta.ProjectManagment.Application.Common.Exceptions;
//using Rasta.ProjectManagment.Application.Common.Interfaces;
//using Rasta.ProjectManagment.Application.Property.Commands.UpdateProperty;
//using Rasta.ProjectManagment.Domain.Events;
//using Rasta.ProjectManagment.Domain.Events.WorkBreakdownStructure;

//namespace Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.UpdateWorkBreakdownStructure;
//public class UpdateWorkBreakdownStructureCommand : IRequest
//{
//    public required int Id { get; set; }
//    public required string Code { get; set; }
//    public required string BudjetCode { get; set; }
//    public required string TitleEn { get; set; }
//    public required string Title { get; set; }
//    public int? ParentId { get; set; }

//    public static implicit operator Domain.Entities.WorkBreakdownStructure(UpdateWorkBreakdownStructureCommand command)
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
//public class UpdateWorkBreakdownStructureCommandHandler : IRequestHandler<UpdateWorkBreakdownStructureCommand>
//{
//    private readonly IApplicationDbContext _context;

//    public UpdateWorkBreakdownStructureCommandHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task Handle(UpdateWorkBreakdownStructureCommand request, CancellationToken cancellationToken)
//    {
//        var entity = await _context.WorkBreakdownStructures.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

//        if (entity == null)
//        {
//            throw new NotFoundException(nameof(Project), request.Id);
//        }

//        entity.Id = request.Id;
//        entity.Code = request.Code;
//        entity.BudjetCode = request.BudjetCode;
//        entity.TitleEn = request.TitleEn;
//        entity.Title = request.Title;
//        entity.ParentId = request.ParentId;


//        entity.AddDomainEvent(new WorkBreakdownStructureEvent(entity));

//        await _context.SaveChangesAsync(cancellationToken);
//    }
//}
