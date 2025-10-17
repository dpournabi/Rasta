namespace Rasta.ProjectManagment.Domain.Entities;

public class BasicInformationProperty:BaseEntity<int>
{
    public required int BasicInformationId { get; set; }
    public  BasicInformation BasicInformation { get; set; }

    public required int PropertyId { get; set; }
    public  Property Property { get; set; }

    public required string Value { get; set; }
}
