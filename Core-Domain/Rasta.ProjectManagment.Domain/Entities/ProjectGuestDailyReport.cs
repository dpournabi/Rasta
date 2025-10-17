namespace Rasta.ProjectManagment.Domain.Entities
{
    /// <summary>
    /// میهمان
    /// </summary>
    public class ProjectGuestDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// نام بازدید کننده
        /// </summary>
        public string VisitorName { get; set; }

        /// <summary>
        /// نام سازمان
        /// </summary>
        public string? OrganizationName { get; set; }

        /// <summary>
        /// زمان ورود
        /// </summary>
        public DateTime EnterTime { get; set; }

        /// <summary>
        /// زمان خروج
        /// </summary>
        public DateTime ExitTime { get; set; }
    }
}
