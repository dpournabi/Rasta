using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Commands.Create;

public record CreateProjectAccidentDailyReportCommand : IRequest<Result<long>>
{
    public required int ProjectId { get; set; }
    public required string Reason { get; set; }
    public required int AccidentTypeId { get; set; }
    public string? AccidentEffect { get; set; }
    public int? DaysLostCount { get; set; }
    public decimal? DamageAmount { get; set; }
    public string? Description { get; set; }

    public static implicit operator Domain.Entities.ProjectAccidentDailyReport(CreateProjectAccidentDailyReportCommand create)
    {
        return new Domain.Entities.ProjectAccidentDailyReport
        {
            ProjectId = create.ProjectId,
            AccidentTypeId = create.AccidentTypeId,
            AccidentEffect = create.AccidentEffect,
            DamageAmount = create.DamageAmount,
            DaysLostCount = create.DaysLostCount,
            Description = create.Description,
            Reason = create.Reason
        };
    }
}

public class CreateProjectAccidentDailyReportCommandHandler : IRequestHandler<CreateProjectAccidentDailyReportCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "درج گزارش حادثه";
    public CreateProjectAccidentDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<long>> Handle(CreateProjectAccidentDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = (Domain.Entities.ProjectAccidentDailyReport)request;
        entity.AddDomainEvent(new ProjectAccidentDailyReportCreatedEvent(entity));
        _context.ProjectAccidentDailyReports.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<long>.Success(message, entity.Id));
    }
}