using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Domain.Entities
{
    public class ProjectUsers: BaseEntity<int>
    {
        public int? ProjectId { get; set; }
        public Guid? UserId { get; set; }
        public long BranchId { get; set; }
    }
}
