using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Commands.Delete;

public record DeleteProjectAccidentDailyReportCommand(long Id) : IRequest<Result<bool>>;
public class DeleteProjectAccidentDailyReportCommandHandler : IRequestHandler<DeleteProjectAccidentDailyReportCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "حذف اطلاعات پروژه";
    public DeleteProjectAccidentDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }
    public async Task<Result<bool>> Handle(DeleteProjectAccidentDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.ProjectAccidentDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        _context.ProjectAccidentDailyReports.Remove(entity);
        entity.AddDomainEvent(new ProjectAccidentDailyReportDeletedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}
