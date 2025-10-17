namespace Rasta.ProjectManagment.Domain.Entities;

public class MeasureUnitMap:BaseEntity<int>
{
    public required int MeasureUnitFromId { get; set; }
    public required MeasureUnit MeasureUnitFrom { get; set; }

    public required int MeasureUnitToId { get; set; }
    public required MeasureUnit MeasureUnitTo { get; set; }

    public required decimal Value { get; set; }
}
