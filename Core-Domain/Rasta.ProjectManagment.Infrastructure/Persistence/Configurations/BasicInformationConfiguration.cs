using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class BasicInformationConfiguration : IEntityTypeConfiguration<BasicInformation>
    {
        public void Configure(EntityTypeBuilder<BasicInformation> builder)
        {
            builder.Property(t => t.Name)
                 .HasMaxLength(100)
                 .IsUnicode();

            builder.Property(t => t.Code)
                .HasMaxLength(50).IsRequired(false);
        }
    }
}
