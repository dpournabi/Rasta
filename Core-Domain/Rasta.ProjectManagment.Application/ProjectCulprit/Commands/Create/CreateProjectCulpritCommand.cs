using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.MeasureUnit.Commands.CreateMeasureUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectCulprit.Commands.Create
{
    public class CreateProjectCulpritCommand: Domain.Entities.ProjectCulprits, IRequest<Result<bool>>
    {

    }

    public class CreateProjectCulpritCommandHandler : IRequestHandler<CreateProjectCulpritCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public CreateProjectCulpritCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(CreateProjectCulpritCommand model, CancellationToken cancellationToken)
        {
            model.CreateDate = DateTime.Now;

            this._context.ProjectCulprits.Add(model);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("Ok", true);
        }
    }
}
