using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Delete;

public record DeleteProjectProblemDailyReportCommand(long Id) : IRequest<Result<bool>>;
public class DeleteProjectProblemDailyReportCommandHandler : IRequestHandler<DeleteProjectProblemDailyReportCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "حذف اطلاعات گزارش روزانه مشکلات";
    public DeleteProjectProblemDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }
    public async Task<Result<bool>> Handle(DeleteProjectProblemDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.ProjectProblemDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        _context.ProjectProblemDailyReports.Remove(entity);
        entity.AddDomainEvent(new ProjectProblemDailyReportDeletedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}
