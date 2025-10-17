using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectWorkBreakdownConfiguration : IEntityTypeConfiguration<ProjectWorkBreakdown>
    {
        public void Configure(EntityTypeBuilder<ProjectWorkBreakdown> builder)
        {
            builder.Property(t => t.BaselineCost).HasPrecision(36, 0);
            builder.Property(t => t.Budjet).HasPrecision(36, 0);
            builder.Property(t => t.CPI).HasPrecision(36, 0);
            builder.Property(t => t.SPI).HasPrecision(36, 0);

            builder.Property(t => t.Title)
               .HasMaxLength(200);


            builder.Property(t => t.Successors)
               .HasMaxLength(200);


            builder.Property(t => t.Predecessors)
               .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1024)
                .IsUnicode();

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
