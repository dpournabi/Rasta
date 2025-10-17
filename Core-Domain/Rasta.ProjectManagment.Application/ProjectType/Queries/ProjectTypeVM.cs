using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectType.Queries;
public class ProjectTypeVM : IMapFrom<Domain.Entities.ProjectType>
{
    public required int Id { get; set; }
    public required string TitleEn { get; set; }
    public required string Title { get; set; }
}
