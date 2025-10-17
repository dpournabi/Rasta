using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.FinancialStatement.Commands.Create
{
    public class CreateFinancialStatementCommand : Domain.Entities.FinancialStatement, IRequest<Result<bool>>
    {

    }

    public class CreateFinancialStatementCommandHandler : IRequestHandler<CreateFinancialStatementCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public CreateFinancialStatementCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(CreateFinancialStatementCommand request, CancellationToken cancellationToken)
        {
            request.CreateDate = DateTime.Now;
            request.IsApproved = false;
            request.ActionId = 0;

            this._context.FinancialStatement.Add(request);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("Ok", true);
        }
    }
}
