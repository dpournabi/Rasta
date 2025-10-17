using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.Common.Models;

public class ProjectDto : IMapFrom<Domain.Entities.Project>
{
    public required string ProjectName { get; set; }
    public required int ProjectTypeId { get; set; }
    public required string EmployerName { get; set; }
    public required int InfrastructureArea { get; set; }
    public required int FloorCount { get; set; }
    public required int UnitCount { get; set; }
    public required DateTime StartContractDate { get; set; }
    public required DateTime EndContractDate { get; set; }
    public required bool IsDelete { get; set; }
}
