using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectType;

namespace Rasta.ProjectManagment.Application.ProjectType.Commands.CreateProjectType;
public class CreateProjectTypeCommand : IRequest<int>
{
    public required string TitleEn { get; set; }
    public required string Title { get; set; }

    public static implicit operator Domain.Entities.ProjectType(CreateProjectTypeCommand create)
    {
        return new Domain.Entities.ProjectType
        {
            Title = create.Title,
            TitleEn = create.TitleEn
        };
    }
}

public class CreateProjectTypeCommandHandler : IRequestHandler<CreateProjectTypeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = (Domain.Entities.ProjectType)request;
        entity.AddDomainEvent(new ProjectTypeCreatedEvent(entity));
        _context.projectTypes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
