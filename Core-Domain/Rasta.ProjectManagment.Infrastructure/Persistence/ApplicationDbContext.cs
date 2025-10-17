using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Entities;
using Rasta.ProjectManagment.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace Rasta.ProjectManagment.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator? _mediator;
        private readonly AuditableEntitySaveChangesInterceptor? _auditableEntitySaveChangesInterceptor;

        #region Ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                       IMediator? mediator = null,
                                       AuditableEntitySaveChangesInterceptor? auditableEntitySaveChangesInterceptor = null)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            //ApplicationDbContext context = new ApplicationDbContext(options);
            //context.ProjectWorkBreakdowns.Where(x => x.ProjectId == 7).ExecuteUpdate()
        }
        #endregion

        public DbSet<Domain.Entities.Project> Projects { get; set; }
        public DbSet<Domain.Entities.ProjectType> projectTypes { get; set; }
        public DbSet<Domain.Entities.ProjectTypeRatio> ProjectTypeRatios { get; set; }
        public DbSet<Domain.Entities.BasicInformation> BasicInformations { get; set; }
        public DbSet<Domain.Entities.BasicInformationProperty> BasicInformationProperties { get; set; }
        public DbSet<Domain.Entities.Property> Properties { get; set; }
        public DbSet<Domain.Entities.MeasureUnit> MeasureUnits { get; set; }
        public DbSet<Domain.Entities.MeasureUnitMap> MeasureUnitMaps { get; set; }
        //public DbSet<Domain.Entities.WorkBreakdownStructure> WorkBreakdownStructures { get; set; }
        public DbSet<Domain.Entities.ProjectWorkBreakdown> ProjectWorkBreakdowns { get; set; }
        public DbSet<Domain.Entities.ProjectWeek> ProjectWeekes { get; set; }
        public DbSet<Domain.Entities.ProjectWorkBreakdownHistoryWork> ProjectWorkBreakdownHistoryWorks { get; set; }
        public DbSet<Domain.Entities.LookUp> LookUps { get; set; }
        public DbSet<Domain.Entities.ProjectExecutionDailyReport> ProjectExecutionDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectAccidentDailyReport> ProjectAccidentDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectGuestDailyReport> ProjectGuestDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectMachineryDailyReport> ProjectMachineryDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectManpowerDailyReport> ProjectManpowerDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectMaterialsDailyReport> ProjectMaterialsDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectProblemDailyReport> ProjectProblemDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectRepairDailyReport> ProjectRepairDailyReports { get; set; }
        public DbSet<Domain.Entities.ProjectCulprits> ProjectCulprits { get; set; }
        public DbSet<Domain.Entities.View_Culprits> View_Culprits { get; set; }
        public DbSet<Domain.Entities.FinancialStatement> FinancialStatement { get; set; }
        public DbSet<SP_GetSummaryLevel3VM> SP_GetSummaryLevel3VMs { get; set; }
        public DbSet<ProjectUsers> ProjectUsers { get; set; }
        public DbSet<View_ProjectUsers> View_ProjectUsers { get; set; }
        public DatabaseFacade GetDatabase() => this.Database;

        #region Configorations
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Domain.Entities.MeasureUnitMap>()
                .HasOne(x => x.MeasureUnitFrom)
                .WithMany(x => x.MeasureUnitFroms)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.MeasureUnitMap>()
                .HasOne(x => x.MeasureUnitTo)
                .WithMany(x => x.MeasureUnitTos)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.BasicInformationProperty>()
               .HasOne(x => x.BasicInformation)
               .WithMany(x => x.BasicInformationProperties)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.BasicInformationProperty>()
               .HasOne(x => x.Property)
               .WithMany(x => x.BasicInformationProperties)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectWorkBreakdownHistoryWork>()
               .HasOne(x => x.ProjectWeek)
               .WithMany(x => x.ProjectWorkBreakdownHistoryWorks)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectWorkBreakdownHistoryWork>()
              .HasOne(x => x.ProjectWorkBreakdown)
              .WithMany(x => x.ProjectWorkBreakdownHistories)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectAccidentDailyReport>()
              .HasOne(x => x.Project)
              .WithMany(x => x.ProjectAccidentDailyReports)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectExecutionDailyReport>()
             .HasOne(x => x.Project)
             .WithMany(x => x.ProjectExecutionDailyReports)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectGuestDailyReport>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectGuestDailyReports)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectMachineryDailyReport>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectMachineryDailyReports)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectManpowerDailyReport>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectManpowerDailyReports)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectMaterialsDailyReport>()
             .HasOne(x => x.Project)
             .WithMany(x => x.ProjectMaterialsDailyReports)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectProblemDailyReport>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectProblemDailyReports)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Domain.Entities.ProjectRepairDailyReport>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectRepairDailyReports)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProjectUsers>().ToTable("ProjectUsers");

            builder.Entity<View_ProjectUsers>().HasNoKey();

            //builder.Entity<WorkBreakdownStructure>().Navigation(e => e.Childrens).AutoInclude(true);

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            SetDataProvider(optionsBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);
            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDesignTimeDbContextFactory
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContextFactory()
            {

            }
            public ApplicationDbContext CreateDbContext(string[] args)
                => new(SetDataProvider(new DbContextOptionsBuilder<ApplicationDbContext>()).Options);

        }
        #endregion

        #region Private Functions
        private static void SetDataProvider(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration _configuration = GetConfiguration();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
        }
        private static DbContextOptionsBuilder<ApplicationDbContext> SetDataProvider(DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder)
        {
            IConfiguration _configuration = GetConfiguration();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
            return optionsBuilder;
        }
        private static IConfiguration GetConfiguration()
        {
#if RELEASE
                        IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Production.json")
                            .Build();
#else
            IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
#endif

            return _configuration;
        }
        #endregion
    }
}
