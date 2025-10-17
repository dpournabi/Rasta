//using MediatR;
//using Rasta.ProjectManagment.Application.Common.Exceptions;
//using Rasta.ProjectManagment.Application.Common.Interfaces;
//using Rasta.ProjectManagment.Domain.Events;
//using Rasta.ProjectManagment.Domain.Events.WorkBreakdownStructure;

//namespace Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.DeleteWorkBreakdownStructure;
//public record DeleteWorkBreakdownStructureCommand(int Id) : IRequest;

//public class DeleteWorkBreakdownStructureCommandHandler : IRequestHandler<DeleteWorkBreakdownStructureCommand>
//{
//    private readonly IApplicationDbContext _context;

//    public DeleteWorkBreakdownStructureCommandHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task Handle(DeleteWorkBreakdownStructureCommand request, CancellationToken cancellationToken)
//    {
//        var entity = await _context.WorkBreakdownStructures
//                .FindAsync(new object[] { request.Id }, cancellationToken);

//        if (entity == null)
//        {
//            throw new NotFoundException(nameof(Project), request.Id);
//        }

//        _context.WorkBreakdownStructures.Remove(entity);

//        entity.AddDomainEvent(new WorkBreakdownStructureDeletedEvent(entity));

//        await _context.SaveChangesAsync(cancellationToken);
//    }
//}

