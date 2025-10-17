namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReport.GetCPIReportWeekly
{
    public class CPIReportWeeklyVM
    {
        public long ProjectId { get; set; }
        public int WeekCount { get; set; }
        public decimal? CPI { get; set; }
    }
}
