using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectCulprit.Commands.Delete
{
    public class DeleteProjectCulpritCommand: IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteProjectCulpritCommandHandler : IRequestHandler<DeleteProjectCulpritCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _DbContext;
        public DeleteProjectCulpritCommandHandler(IApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(DeleteProjectCulpritCommand request, CancellationToken cancellationToken)
        {
            var existing = this._DbContext.ProjectCulprits.Where(x => x.Id == request.Id).FirstOrDefault();

            if (existing != null)
            {
                this._DbContext.ProjectCulprits.Remove(existing);
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
