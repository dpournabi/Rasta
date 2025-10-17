using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Queries.GetProjectWorkBreakdownReport;
public class ProjectWorkBreakdownReportVM
{
    public string WBSCode { get; set; }
    public string Title { get; set; }
    public bool? IsCretical { get; set; }

    //درصد پلن
    public double? PE { get; set; }

    //درصد تجمعی پلن
    public double? PlanCumulativePercentage { get; set; }

    //درصد تکمیل پلن جاری
    public double? PlanCompleteProgressPercentage { get; set; }

    //درصد پیشرفت وزنی پلن جاری
    public double? PlanWeightProgressPercentage { get; set; }

    //درصد واقعی
    public double? AC { get; set; }

    //درصد تجمعی واقعی
    public double? RealCumulativePercentage { get; set; }

    //درصد تکمیل واقعی
    public double? ActualCompleteProgressPercentage { get; set; }

    //درصد پیشرفت وزنی 
    public double? ActualWeightProgressPercentage { get; set; }

    //انحراف هفتگی
    public double? WeeklyDeviation => this.PlanCompleteProgressPercentage - this.ActualCompleteProgressPercentage;

    //انحراف تجمعی
    public double? CumulativeDeviation => this.PlanWeightProgressPercentage - this.ActualWeightProgressPercentage;

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

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set;}
    public string WeekNumber { get; set; }
}
