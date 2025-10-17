using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectExecutionDailyReportConfiguration : IEntityTypeConfiguration<ProjectExecutionDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectExecutionDailyReport> builder)
        {
            builder.Property(t => t.ZoneNo)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            builder.Property(t => t.BlockNo)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(t => t.MainOperation)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired(false);

            builder.Property(t => t.SubOperation)
                   .IsUnicode()
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(t => t.Location)
                    .HasMaxLength(100)
                    .IsUnicode()
                    .IsRequired(false);

            builder.Property(t => t.Description)
                   .IsUnicode()
                   .HasMaxLength(1024)
                   .IsRequired(false);

            builder.Property(t => t.Unit)
                   .IsUnicode()
                   .HasMaxLength(20)
                   .IsRequired(true);

            builder.Property(t => t.SubContractorName)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(t => t.Persons)
                     .IsUnicode()
                     .HasMaxLength(1024)
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
