using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Delete;

public record DeleteProjectMachineryDailyReportCommand(long Id) : IRequest<Result<bool>>;
public class DeleteProjectMachineryDailyReportCommandHandler : IRequestHandler<DeleteProjectMachineryDailyReportCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "حذف اطلاعات گزارش روزانه تعمیرات";
    public DeleteProjectMachineryDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }
    public async Task<Result<bool>> Handle(DeleteProjectMachineryDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.ProjectMachineryDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        _context.ProjectMachineryDailyReports.Remove(entity);
        entity.AddDomainEvent(new ProjectMachineryDailyReportDeletedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}
