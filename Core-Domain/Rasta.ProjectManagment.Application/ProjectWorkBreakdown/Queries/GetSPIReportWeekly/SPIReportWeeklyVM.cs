namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReport.GetSPIReportWeekly
{
    public class SPIReportWeeklyVM
    {
        public long ProjectId { get; set; }
        public int WeekCount { get; set; }
        public decimal? SPI { get; set; }
    }
}
