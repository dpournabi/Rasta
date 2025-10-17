using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectProblemDailyReportConfiguration : IEntityTypeConfiguration<ProjectProblemDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectProblemDailyReport> builder)
        {
            builder.Property(t => t.ProblemsClassifications)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(t => t.Description)
                   .IsUnicode()
                   .HasMaxLength(1024)
                   .IsRequired(true);

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
