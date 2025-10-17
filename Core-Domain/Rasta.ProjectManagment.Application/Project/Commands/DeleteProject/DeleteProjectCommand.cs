using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.Project.Commands.UpdateProject
{
    public record DeleteProjectCommand(long Id) : IRequest<Result<bool>>;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "حذف اطلاعات پروژه";
        public DeleteProjectCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                return await Task.FromResult(Result<bool>.Failure(message, null, false));
            }

            _context.Projects.Remove(entity);
            entity.AddDomainEvent(new ProjectDeletedEvent(entity));
            await _context.SaveChangesAsync(cancellationToken);
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Success(message, true));
        }
    }
}
