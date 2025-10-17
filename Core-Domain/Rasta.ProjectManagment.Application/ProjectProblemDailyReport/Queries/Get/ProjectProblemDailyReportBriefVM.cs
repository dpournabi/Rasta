using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Queries.Get;

public class ProjectProblemDailyReportBriefVM : IMapFrom<Domain.Entities.ProjectProblemDailyReport>
{
    public required long Id { get; set; }
    public required int ProjectId { get; set; }
    public required string ProblemsClassifications { get; set; }
    public required string Description { get; set; }
}
