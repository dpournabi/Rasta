using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectUsers.AssignQuery
{
    public class ProjectUsersAssignQuery : IRequest<Result<IEnumerable<View_ProjectUsers>>>
    {
        public long BranchId { get; set; }
    }

    public class ProjectUsersAssignQueryHandler: IRequestHandler<ProjectUsersAssignQuery, Result<IEnumerable<View_ProjectUsers>>>
    {
        private readonly IApplicationDbContext _context;

        public ProjectUsersAssignQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<View_ProjectUsers>>> Handle(ProjectUsersAssignQuery request, CancellationToken cancellationToken)
        {
            var result = await this._context
                            .View_ProjectUsers.Where(v => v.BranchId == request.BranchId)
                            .OrderBy(v => v.UserName)
                            .ToListAsync();

            return Result<IEnumerable<View_ProjectUsers>>.Success("", result);                
        }
    }
}
