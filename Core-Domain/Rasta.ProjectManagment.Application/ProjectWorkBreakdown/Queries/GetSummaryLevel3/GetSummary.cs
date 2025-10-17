using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSummaryLevel3
{
    public class GetSummary
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? Level { get; set; }
        public required string Code { get; set; }
        public required string Title { get; set; }
        public int? ParentId { get; set; }
        public int? WeekNumber { get; set; }
        public double? PlanCompleteProgressPercentage { get; set; }
        public double? ActualCompleteProgressPercentage { get; set; }
        public bool IsLastNode { get; set; }
    }
}
