namespace Rasta.ProjectManagment.Domain.Entities
{
    public class ProjectAccidentDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// علت وقوع حادثه
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// نوع حادثه
        /// </summary>
        public required int AccidentTypeId { get; set; }
        public LookUp AccidentType { get; set; }

        /// <summary>
        /// اثر حادثه
        /// </summary>
        public string? AccidentEffect { get; set; }

        /// <summary>
        /// تعداد روز از دست رفته
        /// </summary>
        public int? DaysLostCount { get; set; }

        /// <summary>
        /// خسارت وارد شده به شرکت
        /// </summary>
        public decimal? DamageAmount { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string? Description { get; set; }
    }
}
