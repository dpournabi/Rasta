namespace Rasta.ProjectManagment.Domain.Entities
{
    /// <summary>
    /// مصالح
    /// </summary>
    public partial class ProjectMaterialsDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// شرح مصالح
        /// </summary>
        public required string MaterialsDescription { get; set; }

        /// <summary>
        /// واحد
        /// </summary>
        public MeasureUnit Unit { get; set; }
        public required int UnitId { get; set; }

        /// <summary>
        /// مقدار وارده در روز
        /// </summary>
        public required double ImportAmount { get; set; }

        /// <summary>
        /// محل مصرف
        /// </summary>
        public string? UsePlace { get; set; }
    }
}
