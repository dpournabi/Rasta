using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectTypeConfiguration : IEntityTypeConfiguration<ProjectType>
    {
        public void Configure(EntityTypeBuilder<ProjectType> builder)
        {
            builder.Property(t => t.TitleEn)
               .HasMaxLength(20);

            builder.Property(t => t.Title)
            .HasMaxLength(50)
            .IsUnicode();
        }
    }
}
