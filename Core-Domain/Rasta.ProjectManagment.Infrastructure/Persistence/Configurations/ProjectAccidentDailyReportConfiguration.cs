using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectAccidentDailyReportConfiguration : IEntityTypeConfiguration<ProjectAccidentDailyReport>
    {
        public void Configure(EntityTypeBuilder<ProjectAccidentDailyReport> builder)
        {
            builder.Property(t => t.ProjectId)
                   .IsRequired(true);

            builder.Property(t => t.AccidentTypeId)
                .IsRequired(true);

            builder.Property(t => t.Reason)
                   .IsUnicode()
                   .HasMaxLength(512)
                   .IsRequired(true);

            builder.Property(t => t.AccidentEffect)
                  .IsUnicode()
                  .HasMaxLength(256)
                  .IsRequired(false);

            builder.Property(t => t.Description)
                  .IsUnicode()
                  .HasMaxLength(1024)
                  .IsRequired(false);


            builder.Property(t => t.CreatedBy)
                   .IsUnicode()
                   .HasMaxLength(100)
                   .IsRequired(false);

            var converter = new ValueConverter<decimal, double>(
                               v => (double)v,
                               v => (decimal)v
                           );
            builder.Property(t => t.DamageAmount)
                   .HasConversion(converter)
                   .IsRequired(false);


            builder.Property(t => t.LastModifiedBy)
                  .IsUnicode()
                  .HasMaxLength(100)
                  .IsRequired(false);
        }
    }
}
