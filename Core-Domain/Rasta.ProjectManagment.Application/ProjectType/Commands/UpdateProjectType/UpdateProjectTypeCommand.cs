using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectType;

namespace Rasta.ProjectManagment.Application.ProjectType.Commands.UpdateProjectType;
public class UpdateProjectTypeCommand : IRequest
{
    public required int Id { get; set; }
    public required string TitleEn { get; set; }
    public required string Title { get; set; }
}

public class UpdateProjectTypeCommandHandler : IRequestHandler<UpdateProjectTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProjectTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.projectTypes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.Title = request.Title;
        entity.TitleEn = request.TitleEn;

        entity.AddDomainEvent(new ProjectTypeUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
