using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectWeekConfiguration : IEntityTypeConfiguration<ProjectWeek>
    {
        public void Configure(EntityTypeBuilder<ProjectWeek> builder)
        {
            builder.HasQueryFilter(x => !x.IsDelete);

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
