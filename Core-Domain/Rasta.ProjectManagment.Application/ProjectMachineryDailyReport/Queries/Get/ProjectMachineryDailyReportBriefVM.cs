using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Queries.Get;

public record ProjectMachineryDailyReportBriefVM : IMapFrom<Domain.Entities.ProjectMachineryDailyReport>
{
    public required long Id { get; set; }
    public required int ProjectId { get; set; }
    public required string MachineryEquipmentDescription { get; set; }
    public required int WorkingHours { get; set; }
    public required bool IsActive { get; set; }
    public required bool NeedRepair { get; set; }
    public decimal? Total { get; set; }
    public string? Ownership { get; set; }
}
