using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.ProjectType.Commands.CreateProjectType;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.LookUp;

namespace Rasta.ProjectManagment.Application.LookUp.Commands.CreateLookUp;
public class CreateLookUpCommand : IRequest<int>
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }

    public static implicit operator Domain.Entities.LookUp(CreateLookUpCommand create)
    {
        return new Domain.Entities.LookUp
        {
            Code = create.Code,
            Title = create.Title,
            Type = create.Type
        };
    }
}
public class CreateLookUpCommandHandler : IRequestHandler<CreateLookUpCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateLookUpCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateLookUpCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.LookUp)request;
        entity.AddDomainEvent(new LookUpCreatedEvent(entity));
        _context.LookUps.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
