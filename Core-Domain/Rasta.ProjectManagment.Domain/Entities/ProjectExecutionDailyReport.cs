namespace Rasta.ProjectManagment.Domain.Entities
{
    public class ProjectExecutionDailyReport:BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// شماره زون / پارت
        /// </summary>
        public string? ZoneNo { get; set; }

        /// <summary>
        /// شماره بلوک
        /// </summary>
        public string? BlockNo { get; set; }

        /// <summary>
        /// عملیات اصلی
        /// </summary>
        public string? MainOperation { get; set; }

        /// <summary>
        /// عملیات فرعی
        /// </summary>
        public string? SubOperation { get; set; }

        /// <summary>
        /// موقعیت
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// شرح عملیات
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// کارکرد روز
        /// </summary>
        public required int TotalWorkTime { get; set; }

        /// <summary>
        /// کارکرد تجمعی
        /// </summary>
        public required int TotalCumulativeWorkTime { get; set; }

        /// <summary>
        /// واحد
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// نام پیمانکار فرعی
        /// </summary>
        public string? SubContractorName { get; set; }

        /// <summary>
        /// زمان فعالیت(ساعت)
        /// </summary>
        public required int ActivityTime { get; set; }

        /// <summary>
        /// نفرات مستقیم
        /// </summary>
        public string? Persons { get; set; }
    }
}
