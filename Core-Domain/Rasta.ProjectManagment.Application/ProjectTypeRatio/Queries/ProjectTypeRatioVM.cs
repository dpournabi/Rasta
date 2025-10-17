using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectTypeRatio.Queries;
public class ProjectTypeRatioVM : IMapFrom<Domain.Entities.ProjectTypeRatio>
{
    public required int ProjectTypeId { get; set; }
    public required int WFTPercentage { get; set; }
    public required int WFBPercentage { get; set; }
    public required DateTime EffectiveDate { get; set; }
}
