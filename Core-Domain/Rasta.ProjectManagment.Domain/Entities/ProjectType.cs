namespace Rasta.ProjectManagment.Domain.Entities;

public class ProjectType:BaseEntity<int>
{
    public ProjectType()
    {
        this.ProjectTypeRatios = new HashSet<ProjectTypeRatio>();
    }
    public required string TitleEn { get; set; }
    public required string Title { get; set; }

    public ICollection<ProjectTypeRatio> ProjectTypeRatios { get; set; }
}
