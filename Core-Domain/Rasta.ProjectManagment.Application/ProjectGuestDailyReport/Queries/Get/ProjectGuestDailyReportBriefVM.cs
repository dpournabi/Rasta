using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Queries.Get;

public class ProjectGuestDailyReportBriefVM : IMapFrom<Domain.Entities.ProjectAccidentDailyReport>
{
    public required long Id { get; set; }
    public required int ProjectId { get; set; }
    public required string VisitorName { get; set; }
    public string? OrganizationName { get; set; }
    public required DateTime EnterTime { get; set; }
    public required DateTime ExitTime { get; set; }
}
