namespace Rasta.ProjectManagment.Domain.Common
{
    public abstract class BaseEntityWithSoftDelete<T> : BaseEntity<T>
    {
        public bool IsDelete { get; set; }
    }
}
