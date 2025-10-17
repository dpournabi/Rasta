using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.MeasureUnit.Queries;
public class MeasureUnitVM : IMapFrom<Domain.Entities.MeasureUnit>
{
    public required int Id { get; set; }
    public required string TitleEn { get; set; }
    public required string Title { get; set; }
    public required bool IsDefault { get; set; }
}
