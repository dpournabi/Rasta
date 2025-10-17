namespace Rasta.ProjectManagment.Domain.Entities;

public class BasicInformation : BaseEntity<int>
{
    public BasicInformation() 
    {
        this.BasicInformationProperties = new HashSet<BasicInformationProperty>();
    }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required int Level { get; set; }
    public int? MeasureUnitId { get; set; }
    public MeasureUnit? MeasureUnit { get; set; }

    public int? ParentId { get; set; }
    public BasicInformation? Parent { get; set; }

    public ICollection<BasicInformationProperty> BasicInformationProperties { get; set; }
}
