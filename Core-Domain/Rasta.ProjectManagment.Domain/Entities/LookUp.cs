namespace Rasta.ProjectManagment.Domain.Entities;

public class LookUp : BaseEntity<int>
{
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }

}
