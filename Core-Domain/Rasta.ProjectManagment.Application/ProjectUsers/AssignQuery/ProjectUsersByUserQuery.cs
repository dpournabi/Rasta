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
    public class ProjectUsersByUserQuery: IRequest<Result<IEnumerable<View_ProjectUsers>>>
    {
        public int BranchId { get; set; }
        public Guid UserId { get; set; }
    }

    public class ProjectUsersByUserQueryHandler: IRequestHandler<ProjectUsersByUserQuery, Result<IEnumerable<View_ProjectUsers>>>
    {
        private readonly IApplicationDbContext _context;

        public ProjectUsersByUserQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<View_ProjectUsers>>> Handle(ProjectUsersByUserQuery request, CancellationToken cancellationToken)
        {
            var result = await this._context
                            .View_ProjectUsers.Where(v => v.UserId == request.UserId)
                            .ToListAsync();

            return Result<IEnumerable<View_ProjectUsers>>.Success("", result);
        }
    }
}
