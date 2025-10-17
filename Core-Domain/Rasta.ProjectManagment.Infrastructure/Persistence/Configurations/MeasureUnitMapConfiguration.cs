using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class MeasureUnitMapConfiguration : IEntityTypeConfiguration<MeasureUnitMap>
    {
        public void Configure(EntityTypeBuilder<MeasureUnitMap> builder)
        {
            builder.Property(t => t.Value).HasPrecision(18, 2);
        }
    }
}
