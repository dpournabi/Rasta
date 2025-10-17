using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Domain.Entities;

namespace Rasta.ProjectManagment.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DatabaseFacade GetDatabase();
        DbSet<Domain.Entities.Project> Projects { get; }
        DbSet<Domain.Entities.ProjectType> projectTypes { get; }
        DbSet<Domain.Entities.ProjectTypeRatio> ProjectTypeRatios { get; }
        DbSet<Domain.Entities.BasicInformation> BasicInformations { get; }
        DbSet<Domain.Entities.BasicInformationProperty> BasicInformationProperties { get; }
        DbSet<Domain.Entities.Property> Properties { get; }
        DbSet<Domain.Entities.MeasureUnit> MeasureUnits { get; }
        DbSet<Domain.Entities.MeasureUnitMap> MeasureUnitMaps { get; }
        //DbSet<Domain.Entities.WorkBreakdownStructure> WorkBreakdownStructures { get; }
        DbSet<Domain.Entities.ProjectWorkBreakdown> ProjectWorkBreakdowns { get; }
        DbSet<Domain.Entities.ProjectWeek> ProjectWeekes { get; }
        DbSet<Domain.Entities.ProjectWorkBreakdownHistoryWork> ProjectWorkBreakdownHistoryWorks { get; }
        DbSet<Domain.Entities.LookUp> LookUps { get; }
        DbSet<Domain.Entities.ProjectExecutionDailyReport> ProjectExecutionDailyReports { get; }
        DbSet<Domain.Entities.ProjectAccidentDailyReport> ProjectAccidentDailyReports { get; }
        DbSet<Domain.Entities.ProjectGuestDailyReport> ProjectGuestDailyReports { get; }
        DbSet<Domain.Entities.ProjectMachineryDailyReport> ProjectMachineryDailyReports { get; }
        DbSet<Domain.Entities.ProjectManpowerDailyReport> ProjectManpowerDailyReports { get; }
        DbSet<Domain.Entities.ProjectMaterialsDailyReport> ProjectMaterialsDailyReports { get; }
        DbSet<Domain.Entities.ProjectProblemDailyReport> ProjectProblemDailyReports { get; }
        DbSet<Domain.Entities.ProjectRepairDailyReport> ProjectRepairDailyReports { get; }
        DbSet<Domain.Entities.ProjectCulprits> ProjectCulprits { get; }
        DbSet<Domain.Entities.View_Culprits> View_Culprits { get; }
        DbSet<Domain.Entities.FinancialStatement> FinancialStatement { get; }
        DbSet<SP_GetSummaryLevel3VM> SP_GetSummaryLevel3VMs { get; }
        DbSet<Domain.Entities.ProjectUsers> ProjectUsers { get; }
        DbSet<Domain.Entities.View_ProjectUsers> View_ProjectUsers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
