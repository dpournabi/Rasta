using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Domain.Entities
{
    public partial class ProjectMaterialsDailyReport
    {
        [NotMapped]
        public string? UnitTitle { get; set; }
    }
}
