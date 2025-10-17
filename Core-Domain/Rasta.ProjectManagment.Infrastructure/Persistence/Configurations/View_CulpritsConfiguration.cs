using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class View_CulpritsConfiguration : IEntityTypeConfiguration<View_Culprits>
    {
        public void Configure(EntityTypeBuilder<View_Culprits> builder)
        {
            builder.ToTable("View_Culprits");
            builder.HasKey(x => x.Id);
        }
    }
}
