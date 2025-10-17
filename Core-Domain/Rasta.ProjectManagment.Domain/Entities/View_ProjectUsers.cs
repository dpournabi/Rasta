using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Domain.Entities
{
    public class View_ProjectUsers
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public Guid? UserId { get; set; }
        public long BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProjectName { get; set; }
    }
}
