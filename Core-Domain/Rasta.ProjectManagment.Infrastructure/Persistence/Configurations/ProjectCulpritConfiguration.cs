using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations
{
    public class ProjectCulpritConfiguration : IEntityTypeConfiguration<ProjectCulprits>
    {
        public void Configure(EntityTypeBuilder<ProjectCulprits> builder)
        {
            builder.Property(p => p.Description).HasMaxLength(2048);
        }
    }
}
