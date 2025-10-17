using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectManpowerDailyReportConfiguration : IEntityTypeConfiguration<ProjectManpowerDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectManpowerDailyReport> builder)
        {
            builder.Property(t => t.Expertise)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(t => t.Shift1)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(t => t.SubContractorName)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(t => t.Shift2)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired(false);
            builder.Property(t => t.Shift3)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(t => t.IsDirect).IsRequired(true);

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
