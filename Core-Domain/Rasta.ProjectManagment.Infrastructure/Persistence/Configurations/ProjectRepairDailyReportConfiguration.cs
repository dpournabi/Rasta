using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectRepairDailyReportConfiguration : IEntityTypeConfiguration<ProjectRepairDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectRepairDailyReport> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(t => t.RepairPlace)
                   .IsUnicode()
                   .HasMaxLength(100)
                   .IsRequired(true);

            builder.Property(t => t.Description)
                   .IsUnicode()
                   .HasMaxLength(1024)
                   .IsRequired(false);

            builder.Property(t => t.EstimateInitialCost)
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
