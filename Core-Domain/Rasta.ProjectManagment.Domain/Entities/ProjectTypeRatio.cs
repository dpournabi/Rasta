namespace Rasta.ProjectManagment.Domain.Entities;

public class ProjectTypeRatio : BaseEntity<int>
{
    public required int ProjectTypeId { get; set; }
    public  ProjectType ProjectType { get; set; }

    public required int WFTPercentage { get; set; }
    public required int WFBPercentage { get; set; }
    public required DateTime EffectiveDate { get; set; }
}
