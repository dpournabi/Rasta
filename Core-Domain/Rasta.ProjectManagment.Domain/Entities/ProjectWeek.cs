namespace Rasta.ProjectManagment.Domain.Entities;

public class ProjectWeek:BaseAuditableEntityWithSoftDelete<long>
{
    public ProjectWeek()
    {
        this.ProjectWorkBreakdownHistoryWorks = new HashSet<ProjectWorkBreakdownHistoryWork>();
    }
    public required int ProjectId { get; set; }
    public  Project Project { get; set; }

    public int WeekCount { get; set; }
    public int MonthNumber { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required bool IsPlan { get; set; }


    public ICollection<ProjectWorkBreakdownHistoryWork> ProjectWorkBreakdownHistoryWorks { get; set; }
}
