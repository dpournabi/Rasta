//using Rasta.ProjectManagment.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Rasta.ProjectManagment.Infrastructure.Persistence.Configurations;

//public class WorkBreakdownStructureConfiguration : IEntityTypeConfiguration<WorkBreakdownStructure>
//{
//    public void Configure(EntityTypeBuilder<WorkBreakdownStructure> builder)
//    {
//        builder.Property(t => t.Title)
//            .HasMaxLength(100)
//            .IsUnicode();

//        builder.Property(t => t.TitleEn)
//            .HasMaxLength(100);

//        builder.Property(t => t.Code)
//            .HasMaxLength(20);

//        builder.Property(t => t.BudjetCode)
//            .HasMaxLength(20);
//    }
//}
