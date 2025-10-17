using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectUsers.AssignCommand
{
    public class ProjectUsersAssignCommand : IRequest<Result<bool>>
    {
        public Guid UserId { get; set; }
        public int?[] ProjectIds { get; set; }
        public long BranchId { get; set; }
    }

    public class ProjectUsersAssignCommandHandler : IRequestHandler<ProjectUsersAssignCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;

        public ProjectUsersAssignCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(ProjectUsersAssignCommand request, CancellationToken cancellationToken)
        {
            var currentProjects = await this._context.ProjectUsers.Where(p => p.BranchId == request.BranchId
                && p.UserId == request.UserId).ToListAsync();

            this._context.ProjectUsers.RemoveRange(currentProjects);
            await this._context.SaveChangesAsync(cancellationToken);

            List<Domain.Entities.ProjectUsers> list = new List<Domain.Entities.ProjectUsers>();

            if (request.ProjectIds != null)
            {
                foreach (var item in request.ProjectIds)
                {
                    list.Add(new Domain.Entities.ProjectUsers()
                    {
                        ProjectId = item,
                        UserId = request.UserId,
                        BranchId = request.BranchId,
                    });
                }
            }
            else
            {
                list.Add(new Domain.Entities.ProjectUsers()
                {
                    ProjectId = null,
                    UserId = request.UserId,
                    BranchId = request.BranchId,
                });
            }

            this._context.ProjectUsers.AddRange(list);
            await this._context.SaveChangesAsync(cancellationToken);

            //return new Result<bool>(true, "تخصیص پروژه با موفقیت انجام شد.", null, true);
            return Result<bool>.Success("تخصیص پروژه با موفقیت انجام شد.", true);
        }
    }
}
