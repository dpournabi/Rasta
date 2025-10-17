using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get
{
    public class ProjectExecutionDailyReportBriefVM : IMapFrom<Domain.Entities.ProjectExecutionDailyReport>
    {
        public required long Id { get; set; }
        public string? ZoneNo { get; set; }
        public string? BlockNo { get; set; }
        public string? MainOperation { get; set; }
        public string? SubOperation { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int TotalWorkTime { get; set; }//کارکرد
        public int TotalCumulativeWorkTime { get; set; }//کارکرد تجمعی
        public string? Unit { get; set; }//واحد
        public string? SubContractorName { get; set; }
        public int ActivityTime { get; set; }
        public string? Persons { get; set; }
    }
}
