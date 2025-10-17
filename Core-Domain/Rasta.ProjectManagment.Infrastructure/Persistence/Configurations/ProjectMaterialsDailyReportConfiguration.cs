using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectMaterialsDailyReportConfiguration : IEntityTypeConfiguration<ProjectMaterialsDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectMaterialsDailyReport> builder)
        {
            builder.Property(t => t.MaterialsDescription)
                .HasMaxLength(256)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(t => t.UsePlace)
                   .IsUnicode()
                   .HasMaxLength(256)
                   .IsRequired(true);

            builder.Property(t => t.UnitId).IsRequired(true);
            builder.Property(t => t.ImportAmount).IsRequired(true);

            builder.Property(t => t.CreatedBy)
                   .IsUnicode()
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(t => t.LastModifiedBy)
                  .IsUnicode()
                  .HasMaxLength(100)
                  .IsRequired(false);
        }
    }
}
