using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class MeasureUnitConfiguration : IEntityTypeConfiguration<MeasureUnit>
    {
        public void Configure(EntityTypeBuilder<MeasureUnit> builder)
        {
            builder.Property(t => t.TitleEn)
               .HasMaxLength(10);

            builder.Property(t => t.Title)
                .HasMaxLength(50)
                .IsUnicode();
        }
    }
}
