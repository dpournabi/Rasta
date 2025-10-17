using System.ComponentModel.DataAnnotations.Schema;

namespace Rasta.ProjectManagment.Domain.Entities;

public class ProjectWorkBreakdownHistoryWork:BaseEntity<long>
{
    public long ProjectWeekId { get; set; }
    public ProjectWeek ProjectWeek { get; set; }

    public long? ProjectWorkBreakdownId { get; set; }

    [NotMapped]
    public string? WorkBreakdownStructureCode { get; set; }
    public ProjectWorkBreakdown? ProjectWorkBreakdown { get; set; }

    /// <summary>
    /// درصد پلن PE
    /// </summary>
    public double? PlanPercentage { get; set; }

    /// <summary>
    /// درصد واقعی AC
    /// </summary>
    public double? RealPercentage { get; set; }

    /// <summary>
    /// درصد تجمعی پلن
    /// </summary>
    public double? PlanCumulativePercentage { get; set; }

    /// <summary>
    /// درصد تجمعی واقعی
    /// </summary>
    public double? RealCumulativePercentage { get; set; }
    public string? Description { get; set; }

    //Plan
    /// <summary>
    /// درصد تکمیل پلن جاری A(i)=(this.EndDate - PWBStartDate)/PWBDuration
    /// </summary>
    public double? PlanCompleteProgressPercentage { get; set; }

    //Plan
    /// <summary>
    /// درصد تکمیل پلن جاری A(i)=(this.EndDate - PWBStartDate)/PWBDuration
    /// مجموع کارهای هفته های قبل بعلاوه هفته جاری
    /// </summary>
    public double? CumulativePlanCompleteProgressPercentage { get; set; }

    /// <summary>
    /// درصد پیشرفت وزنی پلن جاری B=A(i)*PWBWeightFactor
    /// </summary>
    public double? PlanWeightProgressPercentage { get; set; }
    

    //Actual
    /// <summary>
    /// درصد تکمیل A(i)=(this.EndDate - PWBStartDate)/PWBDuration
    /// </summary>
    public double? ActualCompleteProgressPercentage { get; set; }

    //Actual
    /// <summary>
    /// درصد تکمیل A(i)=(this.EndDate - PWBStartDate)/PWBDuration
    /// مجموع کارهای هفته های قبل بعلاوه هفته جاری
    /// </summary>
    public double? CumulativeActualCompleteProgressPercentage { get; set; }

    /// <summary>
    /// درصد پیشرفت وزنی  B=A(i)*PWBWeightFactor
    /// </summary>
    public double? ActualWeightProgressPercentage { get; set; }

    public bool IsDone { get; set; }

    /// <summary>
    /// Budget(i) * AC
    /// </summary>
    public decimal? EV { get; set; }

    /// <summary>
    /// Budget(i) * PE
    /// </summary>
    public decimal? PV { get; set; }

    /// <summary>
    /// خود کاربر وارد میکنه
    /// </summary>
    public decimal? ACB { get; set; }

    /// <summary>
    /// PV-EV
    /// </summary>
    public decimal? SV { get; set; }

    /// <summary>
    /// ACB-EV
    /// </summary>
    public decimal? CV { get; set; }
}
