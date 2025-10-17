using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetProjectWorkBreakdownWithPagination;
public record ProjectWorkBreakdownVM : IMapFrom<Domain.Entities.ProjectWorkBreakdown>
{
    public long Id { get; set; }
    public required long ProjectId { get; set; }
    public required string WorkBreakdownStructureCode { get; set; }
    public int? WorkBreakdownStructureId { get; set; }
    public long Code { get; set; }
    public required bool IsCritical { get; set; }
    public required double WeightFactor { get; set; }
    public required int EstimatedTime { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required DateTime LastStartDate { get; set; }
    public required DateTime LastEndDate { get; set; }
    public decimal? BaselineCost { get; set; }
    public required string ActivityType { get; set; }
    public string? Description { get; set; }
    public decimal? Budjet { get; set; }

    /// <summary>
    /// Sum(Ev)/Sum(Pv)
    /// </summary>
    public decimal? SPI { get; set; }

    /// <summary>
    /// Sum(Ev)/Sum(ACB)
    /// </summary>
    public decimal? CPI { get; set; }
}
