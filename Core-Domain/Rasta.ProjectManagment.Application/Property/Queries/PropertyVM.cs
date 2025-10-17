using Rasta.ProjectManagment.Application.Common.Mappings;

namespace Rasta.ProjectManagment.Application.Property.Queries;
public class PropertyVM : IMapFrom<Domain.Entities.Property>
{
    public required string Name { get; set; }
    public required string Type { get; set; }
}
