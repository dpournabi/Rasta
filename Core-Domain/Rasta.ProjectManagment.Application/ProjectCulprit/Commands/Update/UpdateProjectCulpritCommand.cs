using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectCulprit.Commands.Update
{
    public class UpdateProjectCulpritCommand: ProjectCulprits, IRequest<Result<bool>>
    {

    }

    public class UpdateProjectCulpritCommandHandler : IRequestHandler<UpdateProjectCulpritCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _DbContext;
        public UpdateProjectCulpritCommandHandler(IApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(UpdateProjectCulpritCommand request, CancellationToken cancellationToken)
        {
            var existing = this._DbContext.ProjectCulprits.Where(x => x.Id == request.Id).FirstOrDefault();

            if (existing != null)
            {
                existing.CulpritPercent = request.CulpritPercent;
                existing.Description = request.Description;
                existing.JobTitleId = request.JobTitleId;

                this._DbContext.ProjectCulprits.Update(existing);
                await this._DbContext.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success("Ok", true);
            }
            else
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }
        }
    }
}
