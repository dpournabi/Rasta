using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class BasicInformationPropertyConfiguration : IEntityTypeConfiguration<BasicInformationProperty>
    {
        public void Configure(EntityTypeBuilder<BasicInformationProperty> builder)
        {
            builder.Property(t => t.Value)
            .HasMaxLength(50)
            .IsUnicode();
        }
    }
}
