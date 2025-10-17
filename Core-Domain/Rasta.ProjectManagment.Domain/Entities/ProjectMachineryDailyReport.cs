namespace Rasta.ProjectManagment.Domain.Entities
{
    /// <summary>
    /// ماشین آلات
    /// </summary>
    public class ProjectMachineryDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// شرح ماشین آلات/تجهیزات
        /// </summary>
        public required string MachineryEquipmentDescription { get; set; }

        /// <summary>
        /// مقدار کارکرد
        /// </summary>
        public required int WorkingHours { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public required bool IsActive { get; set; }

        /// <summary>
        /// نیاز به تعمیر
        /// </summary>
        public required bool NeedRepair { get; set; }

        /// <summary>
        /// مجموع
        /// </summary>
        public decimal? Total { get; set; }

        /// <summary>
        /// مالکیت
        /// </summary>
        public string? Ownership { get; set; }
    }
}
