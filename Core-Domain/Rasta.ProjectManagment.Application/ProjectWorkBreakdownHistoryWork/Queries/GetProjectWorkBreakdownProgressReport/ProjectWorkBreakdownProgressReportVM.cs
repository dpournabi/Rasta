using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Queries.GetProjectWorkBreakdownProgressReport;
public class ProjectWorkBreakdownProgressReportVM : IMapFrom<Domain.Entities.ProjectWeek>
{
    public int WeekCount { get; set; }
    public double? PlanPercentage { get; set; }
    public double? RealPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public double? CumulativePlanCompleteProgressPercentage { get; set; }
    public double? CumulativeActualCompleteProgressPercentage { get; set; }
}
