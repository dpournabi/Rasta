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
    public class FinancialStatementConfiguration : IEntityTypeConfiguration<FinancialStatement>
    {
        public void Configure(EntityTypeBuilder<FinancialStatement> builder)
        {
            builder.ToTable(nameof(FinancialStatement));
        }
    }
}
