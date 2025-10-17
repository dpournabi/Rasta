using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class SP_GetSummaryLevel3Configuration : IEntityTypeConfiguration<SP_GetSummaryLevel3VM>
    {
        public void Configure(EntityTypeBuilder<SP_GetSummaryLevel3VM> builder)
        {
            builder.HasNoKey();
        }
    }
}
