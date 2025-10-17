namespace Rasta.ProjectManagment.Domain.Entities
{
    /// <summary>
    /// نیروی انسانی
    /// </summary>
    public class ProjectManpowerDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// نیروی انسانی به تفکیک تخصص
        /// </summary>
        public string? Expertise { get; set; }

        /// <summary>
        /// مستقیم/غیرمستقیم
        /// </summary>
        public bool IsDirect { get; set; }

        /// <summary>
        /// شیفت 1
        /// </summary>
        public string? Shift1 { get; set; }

        /// <summary>
        /// شیفت 2
        /// </summary>
        public string? Shift2 { get; set; }

        /// <summary>
        /// شیفت 3
        /// </summary>
        public string? Shift3 { get; set; }

        /// <summary>
        /// مجموع
        /// </summary>
        public decimal? Total { get; set; }

        /// <summary>
        /// نام پیمانکار فرعی
        /// </summary>
        public string? SubContractorName { get; set; }
    }
}
