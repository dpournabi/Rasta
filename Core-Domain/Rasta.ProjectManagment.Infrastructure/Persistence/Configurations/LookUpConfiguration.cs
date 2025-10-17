using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class LookUpConfiguration : IEntityTypeConfiguration<LookUp>
    {
        public void Configure(EntityTypeBuilder<LookUp> builder)
        {
            builder.Property(t => t.Title)
                 .HasMaxLength(100)
                 .IsUnicode();

            builder.Property(t => t.Code)
                .HasMaxLength(10);

            builder.Property(t => t.Type)
                .HasMaxLength(50);

        }
    }
}
