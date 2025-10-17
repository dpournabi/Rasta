using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Create;

public record CreateProjectMachineryDailyReportCommand : IRequest<Result<long>>
{
    public required int ProjectId { get; set; }
    public required string MachineryEquipmentDescription { get; set; }
    public required int WorkingHours { get; set; }
    public required bool IsActive { get; set; }
    public required bool NeedRepair { get; set; }
    public decimal? Total { get; set; }
    public string? Ownership { get; set; }

    public static implicit operator Domain.Entities.ProjectMachineryDailyReport(CreateProjectMachineryDailyReportCommand create)
    {
        return new Domain.Entities.ProjectMachineryDailyReport
        {
            ProjectId = create.ProjectId,
            MachineryEquipmentDescription = create.MachineryEquipmentDescription,
            WorkingHours = create.WorkingHours,
            IsActive = create.IsActive,
            NeedRepair = create.NeedRepair,
            Total = create.Total,
            Ownership = create.Ownership
        };
    }
}

public class CreateProjectMachineryDailyReportCommandHandler : IRequestHandler<CreateProjectMachineryDailyReportCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "درج گزارش روزانه تجهیزات";
    public CreateProjectMachineryDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<long>> Handle(CreateProjectMachineryDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = (Domain.Entities.ProjectMachineryDailyReport)request;
        entity.AddDomainEvent(new ProjectMachineryDailyReportCreatedEvent(entity));
        _context.ProjectMachineryDailyReports.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<long>.Success(message, entity.Id));
    }
}