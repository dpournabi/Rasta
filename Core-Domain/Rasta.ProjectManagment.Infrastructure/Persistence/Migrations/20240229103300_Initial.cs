using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasta.ProjectManagment.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IntegratedSent = table.Column<long>(type: "bigint", nullable: false),
                    PeriodSent = table.Column<long>(type: "bigint", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntegratedApproved = table.Column<long>(type: "bigint", nullable: false),
                    PeriodApproved = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BalancedIntegratedApproved = table.Column<long>(type: "bigint", nullable: false),
                    BalancedPeriodApproved = table.Column<long>(type: "bigint", nullable: false),
                    BalancedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperationYear = table.Column<int>(type: "int", nullable: false),
                    OperationMonth = table.Column<short>(type: "smallint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCulprits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false),
                    CulpritPercent = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCulprits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SP_GetSummaryLevel3VMs",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanPrecentage = table.Column<double>(type: "float", nullable: true),
                    RealPrecentage = table.Column<double>(type: "float", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "View_Culprits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: false),
                    CulpritPercent = table.Column<int>(type: "int", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View_Culprits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "View_ProjectUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BasicInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MeasureUnitId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicInformations_BasicInformations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BasicInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasicInformations_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnitMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasureUnitFromId = table.Column<int>(type: "int", nullable: false),
                    MeasureUnitToId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnitMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasureUnitMaps_MeasureUnits_MeasureUnitFromId",
                        column: x => x.MeasureUnitFromId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeasureUnitMaps_MeasureUnits_MeasureUnitToId",
                        column: x => x.MeasureUnitToId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfrastructureArea = table.Column<int>(type: "int", nullable: false),
                    LandscapeArea = table.Column<int>(type: "int", nullable: false),
                    FloorCount = table.Column<int>(type: "int", nullable: false),
                    UnitCount = table.Column<int>(type: "int", nullable: false),
                    StartContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractBasisIndex = table.Column<long>(type: "bigint", nullable: true),
                    BlockCount = table.Column<int>(type: "int", nullable: true),
                    ContractAmount = table.Column<decimal>(type: "decimal(36,2)", precision: 36, scale: 2, nullable: true),
                    HasBuyPlan = table.Column<bool>(type: "bit", nullable: true),
                    HasExecutionPlan = table.Column<bool>(type: "bit", nullable: true),
                    HasCostBudjet = table.Column<bool>(type: "bit", nullable: true),
                    PositiveFloorCount = table.Column<int>(type: "int", nullable: true),
                    NegativeFloorCount = table.Column<int>(type: "int", nullable: true),
                    HasGroundFloor = table.Column<bool>(type: "bit", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_projectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "projectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypeRatios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    WFTPercentage = table.Column<int>(type: "int", nullable: false),
                    WFBPercentage = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypeRatios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTypeRatios_projectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "projectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicInformationProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicInformationId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInformationProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicInformationProperties_BasicInformations_BasicInformationId",
                        column: x => x.BasicInformationId,
                        principalTable: "BasicInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasicInformationProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectAccidentDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AccidentTypeId = table.Column<int>(type: "int", nullable: false),
                    AccidentEffect = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DaysLostCount = table.Column<int>(type: "int", nullable: true),
                    DamageAmount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAccidentDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAccidentDailyReports_LookUps_AccidentTypeId",
                        column: x => x.AccidentTypeId,
                        principalTable: "LookUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectAccidentDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectExecutionDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ZoneNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BlockNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MainOperation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubOperation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TotalWorkTime = table.Column<int>(type: "int", nullable: false),
                    TotalCumulativeWorkTime = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SubContractorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActivityTime = table.Column<int>(type: "int", nullable: false),
                    Persons = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectExecutionDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectExecutionDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectGuestDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    VisitorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EnterTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGuestDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectGuestDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectMachineryDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    MachineryEquipmentDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    WorkingHours = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NeedRepair = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    Ownership = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMachineryDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMachineryDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectManpowerDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDirect = table.Column<bool>(type: "bit", nullable: false),
                    Shift1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Shift2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Shift3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    SubContractorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectManpowerDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectManpowerDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectMaterialsDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    MaterialsDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ImportAmount = table.Column<double>(type: "float", nullable: false),
                    UsePlace = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMaterialsDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMaterialsDailyReports_MeasureUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMaterialsDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectProblemDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProblemsClassifications = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProblemDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProblemDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectRepairDailyReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RepairStatus = table.Column<bool>(type: "bit", nullable: false),
                    RepairPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstimateInitialCost = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRepairDailyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRepairDailyReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectWeekes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    WeekCount = table.Column<int>(type: "int", nullable: false),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPlan = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWeekes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWeekes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWorkBreakdowns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentTaskId = table.Column<int>(type: "int", nullable: false),
                    ParentTaskId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    WorkBreakdownStructureId = table.Column<int>(type: "int", nullable: true),
                    WorkBreakdownStructureCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    LastStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaselineCost = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Predecessors = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Successors = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsLastNode = table.Column<bool>(type: "bit", nullable: false),
                    WeightFactorTime = table.Column<double>(type: "float", nullable: true),
                    WeightFactorBudject = table.Column<double>(type: "float", nullable: true),
                    WeightFactor = table.Column<double>(type: "float", nullable: true),
                    Budjet = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    SPI = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    CPI = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorkBreakdowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWorkBreakdowns_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWorkBreakdownHistoryWorks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectWeekId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectWorkBreakdownId = table.Column<long>(type: "bigint", nullable: true),
                    PlanPercentage = table.Column<double>(type: "float", nullable: true),
                    RealPercentage = table.Column<double>(type: "float", nullable: true),
                    PlanCumulativePercentage = table.Column<double>(type: "float", nullable: true),
                    RealCumulativePercentage = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanCompleteProgressPercentage = table.Column<double>(type: "float", nullable: true),
                    CumulativePlanCompleteProgressPercentage = table.Column<double>(type: "float", nullable: true),
                    PlanWeightProgressPercentage = table.Column<double>(type: "float", nullable: true),
                    ActualCompleteProgressPercentage = table.Column<double>(type: "float", nullable: true),
                    CumulativeActualCompleteProgressPercentage = table.Column<double>(type: "float", nullable: true),
                    ActualWeightProgressPercentage = table.Column<double>(type: "float", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    EV = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    PV = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    ACB = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    SV = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true),
                    CV = table.Column<decimal>(type: "decimal(36,0)", precision: 36, scale: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorkBreakdownHistoryWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWorkBreakdownHistoryWorks_ProjectWeekes_ProjectWeekId",
                        column: x => x.ProjectWeekId,
                        principalTable: "ProjectWeekes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectWorkBreakdownHistoryWorks_ProjectWorkBreakdowns_ProjectWorkBreakdownId",
                        column: x => x.ProjectWorkBreakdownId,
                        principalTable: "ProjectWorkBreakdowns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformationProperties_BasicInformationId",
                table: "BasicInformationProperties",
                column: "BasicInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformationProperties_PropertyId",
                table: "BasicInformationProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformations_MeasureUnitId",
                table: "BasicInformations",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicInformations_ParentId",
                table: "BasicInformations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnitMaps_MeasureUnitFromId",
                table: "MeasureUnitMaps",
                column: "MeasureUnitFromId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnitMaps_MeasureUnitToId",
                table: "MeasureUnitMaps",
                column: "MeasureUnitToId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAccidentDailyReports_AccidentTypeId",
                table: "ProjectAccidentDailyReports",
                column: "AccidentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAccidentDailyReports_ProjectId",
                table: "ProjectAccidentDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectExecutionDailyReports_ProjectId",
                table: "ProjectExecutionDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGuestDailyReports_ProjectId",
                table: "ProjectGuestDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMachineryDailyReports_ProjectId",
                table: "ProjectMachineryDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManpowerDailyReports_ProjectId",
                table: "ProjectManpowerDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMaterialsDailyReports_ProjectId",
                table: "ProjectMaterialsDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMaterialsDailyReports_UnitId",
                table: "ProjectMaterialsDailyReports",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProblemDailyReports_ProjectId",
                table: "ProjectProblemDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRepairDailyReports_ProjectId",
                table: "ProjectRepairDailyReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeId",
                table: "Projects",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypeRatios_ProjectTypeId",
                table: "ProjectTypeRatios",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWeekes_ProjectId",
                table: "ProjectWeekes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkBreakdownHistoryWorks_ProjectWeekId",
                table: "ProjectWorkBreakdownHistoryWorks",
                column: "ProjectWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkBreakdownHistoryWorks_ProjectWorkBreakdownId",
                table: "ProjectWorkBreakdownHistoryWorks",
                column: "ProjectWorkBreakdownId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkBreakdowns_ProjectId",
                table: "ProjectWorkBreakdowns",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicInformationProperties");

            migrationBuilder.DropTable(
                name: "FinancialStatement");

            migrationBuilder.DropTable(
                name: "MeasureUnitMaps");

            migrationBuilder.DropTable(
                name: "ProjectAccidentDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectCulprits");

            migrationBuilder.DropTable(
                name: "ProjectExecutionDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectGuestDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectMachineryDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectManpowerDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectMaterialsDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectProblemDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectRepairDailyReports");

            migrationBuilder.DropTable(
                name: "ProjectTypeRatios");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "ProjectWorkBreakdownHistoryWorks");

            migrationBuilder.DropTable(
                name: "SP_GetSummaryLevel3VMs");

            migrationBuilder.DropTable(
                name: "View_Culprits");

            migrationBuilder.DropTable(
                name: "View_ProjectUsers");

            migrationBuilder.DropTable(
                name: "BasicInformations");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "LookUps");

            migrationBuilder.DropTable(
                name: "ProjectWeekes");

            migrationBuilder.DropTable(
                name: "ProjectWorkBreakdowns");

            migrationBuilder.DropTable(
                name: "MeasureUnits");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "projectTypes");
        }
    }
}
