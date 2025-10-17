using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Application.Project.Queries.GetProjectWithPagination
{
    public class ProjectBriefVM : IMapFrom<Domain.Entities.Project>
    {
        public required long Id { get; set; }
        public required string ProjectName { get; set; }
        public required int ProjectTypeId { get; set; }
        public required string ProjectTypeTitle { get; set; }
        public required string EmployerName { get; set; }
        public required int InfrastructureArea { get; set; }
        public required int FloorCount { get; set; }
        public required int UnitCount { get; set; }
        public required DateTime StartContractDate { get; set; }
        public required DateTime EndContractDate { get; set; }
        public required string Abbr { get; set; }
        public int? ProvinceId { get; set; }
        public string? ProvinceName { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public string? ProjectManager { get; set; }
        public string? Supervisor { get; set; }
        public required int LandscapeArea { get; set; }
        public long? ContractBasisIndex { get; set; }
        public int? BlockCount { get; set; }
        public decimal? ContractAmount { get; set; }
        public bool? HasBuyPlan { get; set; }
        public bool? HasExecutionPlan { get; set; }
        public bool? HasCostBudjet { get; set; }
    }
}
