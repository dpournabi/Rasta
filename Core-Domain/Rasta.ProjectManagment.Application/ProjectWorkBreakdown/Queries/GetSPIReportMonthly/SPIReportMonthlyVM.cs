namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSPIReportMonthly
{
    public class SPIReportMonthlyVM
    {
        public long ProjectId { get; set; }
        public int MonthNumber { get; set; }
        public decimal? SPI { get; set; }
    }
}
