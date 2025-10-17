namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReportMonthly
{
    public class CPIReportMonthlyVM
    {
        public long ProjectId { get; set; }
        public int MonthNumber { get; set; }
        public decimal? CPI { get; set; }
    }
}
