using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectWorkBreakdownHistoryWorkConfiguration : IEntityTypeConfiguration<ProjectWorkBreakdownHistoryWork>
    {
        public void Configure(EntityTypeBuilder<ProjectWorkBreakdownHistoryWork> builder)
        {
            builder.Property(t => t.EV).HasPrecision(36, 0);
            builder.Property(t => t.PV).HasPrecision(36, 0);
            builder.Property(t => t.ACB).HasPrecision(36, 0);
            builder.Property(t => t.SV).HasPrecision(36, 0);
            builder.Property(t => t.CV).HasPrecision(36, 0);
        }
    }
}
