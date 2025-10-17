using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Delete;

public record DeleteProjectGuestDailyReportCommand(long Id) : IRequest<Result<bool>>;
public class DeleteProjectGuestDailyReportCommandHandler : IRequestHandler<DeleteProjectGuestDailyReportCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "حذف اطلاعات گزارش روزانه میهمان";
    public DeleteProjectGuestDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }
    public async Task<Result<bool>> Handle(DeleteProjectGuestDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.ProjectGuestDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        _context.ProjectGuestDailyReports.Remove(entity);
        entity.AddDomainEvent(new ProjectGuestDailyReportDeletedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}
