using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Domain.Entities
{
    public class View_Culprits
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int JobTitleId { get; set; }
        public int CulpritPercent { get; set; }
        public int WeekNumber { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ProjectName { get; set; }
        public string? JobTitle { get; set; }
    }
}
