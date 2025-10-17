using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Queries;
public class BasicInformationPropertyVM : IMapFrom<Domain.Entities.BasicInformationProperty>
{
    public required int BasicInformationId { get; set; }
    public required Domain.Entities.BasicInformation BasicInformation { get; set; }

    public required int PropertyId { get; set; }
    public required Domain.Entities.Property Property { get; set; }

    public required string Value { get; set; }
}
