using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetProjectWeekWithPagination;
public class ProjectWeekVM : IMapFrom<Domain.Entities.ProjectWeek>
{
    public required long ProjectWorkBreakdownId { get; set; }
    public required int ProcessPercentage { get; set; }
}
