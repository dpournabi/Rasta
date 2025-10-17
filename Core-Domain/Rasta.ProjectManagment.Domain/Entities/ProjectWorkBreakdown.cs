namespace Rasta.ProjectManagment.Domain.Entities;

public class ProjectWorkBreakdown : BaseAuditableEntityWithSoftDelete<long>
{
    public ProjectWorkBreakdown()
    {
        this.ProjectWorkBreakdownHistories = new HashSet<ProjectWorkBreakdownHistoryWork>();
    }

    public int CurrentTaskId { get; set; }
    public int? ParentTaskId { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int? WorkBreakdownStructureId { get; set; }
    public string WorkBreakdownStructureCode { get; set; }
    //public  WorkBreakdownStructure WorkBreakdownStructure { get; set; }

    public bool IsCritical { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? Floor { get; set; }
    public int Duration { get; set; }
    public DateTime LastStartDate { get; set; }
    public DateTime LastEndDate { get; set; }
    public decimal? BaselineCost { get; set; }
    public string Title { get; set; }
    public string? Predecessors { get; set; }
    public string? Successors { get; set; }
    public string? Description { get; set; }
    public bool IsLastNode { get; set; }
    public double? WeightFactorTime { get; set; }
    public double? WeightFactorBudject { get; set; }
    public double? WeightFactor { get; set; }
    public decimal? Budjet { get; set; }

    /// <summary>
    /// Sum(Ev)/Sum(Pv)
    /// </summary>
    public decimal? SPI { get; set; }

    /// <summary>
    /// Sum(Ev)/Sum(ACB)
    /// </summary>
    public decimal? CPI { get; set; }

    public int? Level { get; set; }

    public ICollection<ProjectWorkBreakdownHistoryWork> ProjectWorkBreakdownHistories { get; set; }
}
