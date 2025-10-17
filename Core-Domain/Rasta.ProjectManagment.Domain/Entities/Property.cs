namespace Rasta.ProjectManagment.Domain.Entities;

public class Property:BaseEntity<int>
{
    public Property() 
    {
        this.BasicInformationProperties = new HashSet<BasicInformationProperty>();
    }
    public required string Name { get; set; }
    public required string Type { get; set; }

    public ICollection<BasicInformationProperty> BasicInformationProperties { get; set; }
}
