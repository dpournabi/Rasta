namespace Rasta.ProjectManagment.Domain.Entities
{
    /// <summary>
    /// مشکلات
    /// </summary>
    public class ProjectProblemDailyReport : BaseAuditableEntityWithSoftDelete<long>
    {
        public required int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// دسته بندی موانع و مشکلات پروژه
        /// </summary>
        public string ProblemsClassifications { get; set; }

        /// <summary>
        /// شرح موانع و مشکلات پروژه
        /// </summary>
        public string Description { get; set; }
    }
}
