using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Delete
{
    public record DeleteProjectExecutionDailyReportCommand(long Id) : IRequest<Result<bool>>;

    public class DeleteProjectExecutionDailyReportCommandHandler : IRequestHandler<DeleteProjectExecutionDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "حذف اطلاعات پروژه";
        public DeleteProjectExecutionDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<bool>> Handle(DeleteProjectExecutionDailyReportCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            var entity = await _context.ProjectExecutionDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                return await Task.FromResult(Result<bool>.Failure(message, null, false));
            }

            _context.ProjectExecutionDailyReports.Remove(entity);
            entity.AddDomainEvent(new ProjectDailyReportDeletedEvent(entity));
            await _context.SaveChangesAsync(cancellationToken);
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Success(message, true));
        }
    }
}
