using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Queries.Get;

public class ProjectAccidentDailyReportBriefVM : IMapFrom<Domain.Entities.ProjectAccidentDailyReport>
{
    public required long Id { get; set; }
    public int ProjectId { get; set; }
    public string? Reason { get; set; }
    public int AccidentTypeId { get; set; }
    public string? AccidentEffect { get; set; }
    public int? DaysLostCount { get; set; }
    public decimal? DamageAmount { get; set; }
    public string? Description { get; set; }
    public Rasta.ProjectManagment.Domain.Entities.LookUp AccidentType { get; set; }
}
