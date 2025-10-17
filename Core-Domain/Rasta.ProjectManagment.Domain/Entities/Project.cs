namespace Rasta.ProjectManagment.Domain.Entities;

public class Project : BaseEntityWithSoftDelete<int>
{
    public Project()
    {
        this.ProjectWeeks = new HashSet<ProjectWeek>();
    }

    /// <summary>
    /// نام اختصاری
    /// </summary>
    public required string Abbr { get; set; }

    /// <summary>
    /// نام پروژه
    /// </summary>
    public required string ProjectName { get; set; }

    /// <summary>
    /// نوع پروژه: پیمانکاری
    /// </summary>
    public required int ProjectTypeId { get; set; }
    public ProjectType? ProjectType { get; set; }

    public int? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }

    /// <summary>
    /// کارفرما
    /// </summary>
    public required string EmployerName { get; set; }

    /// <summary>
    /// مدیر پروژه
    /// </summary>
    public string? ProjectManager { get; set; }

    /// <summary>
    /// سرپرست کارگاه
    /// </summary>
    public string? Supervisor { get; set; }

    /// <summary>
    /// متراژ زیربنا متر مربع
    /// </summary>
    public required int InfrastructureArea { get; set; }

    /// <summary>
    /// مساحت محوطه
    /// </summary>
    public required int LandscapeArea { get; set; }

    /// <summary>
    /// تعداد طبقات
    /// </summary>
    public required int FloorCount { get; set; }

    /// <summary>
    /// تعدا واحد
    /// </summary>
    public required int UnitCount { get; set; }

    /// <summary>
    /// تاریخ شروع قرارداد
    /// </summary>
    public required DateTime StartContractDate { get; set; }

    /// <summary>
    /// تاریخ پایان قرارداد
    /// </summary>
    public required DateTime EndContractDate { get; set; }

    /// <summary>
    /// شاخص مبنای قرارداد
    /// </summary>
    public long? ContractBasisIndex { get; set; }

    /// <summary>
    /// تعداد بلوک
    /// </summary>
    public int? BlockCount { get; set; }

    /// <summary>
    /// مبلغ قرارداد
    /// </summary>
    public decimal? ContractAmount { get; set; }

    public bool? HasBuyPlan { get; set; }
    public bool? HasExecutionPlan { get; set; }
    public bool? HasCostBudjet { get; set; }

    //تعداد طبقات مثبت
    public int? PositiveFloorCount { get; set; }

    //تعداد طبقات منفی
    public int? NegativeFloorCount { get; set; }

    //آیا طبقه همکف دارد؟
    public bool? HasGroundFloor { get; set; }

    public ICollection<ProjectWeek> ProjectWeeks { get; set; }
    public ICollection<ProjectAccidentDailyReport> ProjectAccidentDailyReports { get; set; }
    public ICollection<ProjectRepairDailyReport> ProjectRepairDailyReports { get; set; }
    public ICollection<ProjectExecutionDailyReport> ProjectExecutionDailyReports { get; set; }
    public ICollection<ProjectGuestDailyReport> ProjectGuestDailyReports { get; set; }
    public ICollection<ProjectMachineryDailyReport> ProjectMachineryDailyReports { get; set; }
    public ICollection<ProjectManpowerDailyReport> ProjectManpowerDailyReports { get; set; }
    public ICollection<ProjectMaterialsDailyReport> ProjectMaterialsDailyReports { get; set; }
    public ICollection<ProjectProblemDailyReport> ProjectProblemDailyReports { get; set; }
}
