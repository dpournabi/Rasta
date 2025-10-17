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
    public class ProjectUsersDeleteCommand: IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public  class ProjectUsersDeleteCommandHandler: IRequestHandler<ProjectUsersDeleteCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;

        public ProjectUsersDeleteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(ProjectUsersDeleteCommand request, CancellationToken cancellationToken)
        {
            var existing = await this._context.ProjectUsers.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (existing != null)
            {
                this._context.ProjectUsers.Remove(existing);
                await this._context.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success("با موفقیت انجام شد.", true);
            }

            return Result<bool>.Failure("بروز خطا!", null, false);
        }
    }
}
