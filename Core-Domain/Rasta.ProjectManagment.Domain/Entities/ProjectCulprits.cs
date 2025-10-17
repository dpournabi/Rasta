using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Domain.Entities
{
    public partial class ProjectCulprits
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int JobTitleId { get; set; }
        public int CulpritPercent { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int WeekNumber { get; set; }
    }
}
