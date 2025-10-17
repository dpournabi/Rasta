using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Create;

public record CreateProjectGuestDailyReportCommand : IRequest<Result<long>>
{
    public required int ProjectId { get; set; }
    public required string VisitorName { get; set; }
    public string? OrganizationName { get; set; }
    public required DateTime EnterTime { get; set; }
    public required DateTime ExitTime { get; set; }

    public static implicit operator Domain.Entities.ProjectGuestDailyReport(CreateProjectGuestDailyReportCommand create)
    {
        return new Domain.Entities.ProjectGuestDailyReport
        {
            ProjectId = create.ProjectId,
            VisitorName = create.VisitorName,
            OrganizationName = create.OrganizationName,
            EnterTime = create.EnterTime,
            ExitTime = create.ExitTime
        };
    }
}

public class CreateProjectGuestDailyReportCommandHandler : IRequestHandler<CreateProjectGuestDailyReportCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "درج گزارش میهمان";
    public CreateProjectGuestDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<long>> Handle(CreateProjectGuestDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = (Domain.Entities.ProjectGuestDailyReport)request;
        entity.AddDomainEvent(new ProjectGuestDailyReportCreatedEvent(entity));
        _context.ProjectGuestDailyReports.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<long>.Success(message, entity.Id));
    }
}