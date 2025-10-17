using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectGuestDailyReportConfiguration : IEntityTypeConfiguration<ProjectGuestDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectGuestDailyReport> builder)
        {
            builder.Property(t => t.VisitorName)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(t => t.OrganizationName)
                   .IsUnicode()
                   .HasMaxLength(256)
                   .IsRequired(false);

            builder.Property(t => t.EnterTime).IsRequired(true);
            builder.Property(t => t.ExitTime).IsRequired(true);

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
