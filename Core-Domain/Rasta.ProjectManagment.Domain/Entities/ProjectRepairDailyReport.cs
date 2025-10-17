namespace Rasta.ProjectManagment.Domain.Entities
{
    public class ProjectRepairDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// شرح ماشین/تجهیزات
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// وضعیت تعمیرات
        /// </summary>
        public bool RepairStatus { get; set; }

        /// <summary>
        /// محل انجام تعمیرات
        /// </summary>
        public required string RepairPlace { get; set; }

        /// <summary>
        /// برآورد هزینه اولیه تعمیرات
        /// </summary>
        public decimal? EstimateInitialCost { get; set; }

        /// <summary>
        /// شرح تعمیرات
        /// </summary>
        public string? Description { get; set; }
    }
}
