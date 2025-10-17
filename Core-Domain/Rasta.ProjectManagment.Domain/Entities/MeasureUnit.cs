namespace Rasta.ProjectManagment.Domain.Entities;

public class MeasureUnit : BaseEntity<int>
{
    public MeasureUnit()
    {
        this.MeasureUnitFroms = new HashSet<MeasureUnitMap>();
        this.MeasureUnitTos = new HashSet<MeasureUnitMap>();
    }
    public required string TitleEn { get; set; }
    public required string Title { get; set; }
    public required bool IsDefault { get; set; }

    public ICollection<MeasureUnitMap> MeasureUnitFroms { get; set; }

    public ICollection<MeasureUnitMap> MeasureUnitTos { get; set; }
}
