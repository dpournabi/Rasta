using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectMachineryDailyReportConfiguration : IEntityTypeConfiguration<ProjectMachineryDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectMachineryDailyReport> builder)
        {
            builder.Property(t => t.MachineryEquipmentDescription)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(t => t.Ownership)
                   .IsUnicode()
                   .HasMaxLength(256)
                   .IsRequired(false);

            builder.Property(t => t.WorkingHours).IsRequired(true);
            builder.Property(t => t.IsActive).IsRequired(true);
            builder.Property(t => t.NeedRepair).IsRequired(true);

            builder.Property(t => t.Total)
                   .HasPrecision(36, 0)
                   .IsRequired(false);

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
