using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.BasicInformation.Queries;
public class BasicInformationVM : IMapFrom<Domain.Entities.BasicInformation>
{
    public BasicInformationVM()
    {
        Childrens = new HashSet<BasicInformationVM>();
    }
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required int Level { get; set; }
    public int? MeasureUnitId { get; set; }
    public int? ParentId { get; set; }

    public ICollection<BasicInformationVM> Childrens;
}
