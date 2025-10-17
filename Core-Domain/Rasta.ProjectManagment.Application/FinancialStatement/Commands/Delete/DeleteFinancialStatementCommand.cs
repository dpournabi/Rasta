using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.FinancialStatement.Commands.Delete
{
    public class DeleteFinancialStatementCommand: IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteFinancialStatementCommandHandler: IRequestHandler<DeleteFinancialStatementCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _DbContext;
        public DeleteFinancialStatementCommandHandler(IApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(DeleteFinancialStatementCommand request, CancellationToken cancellationToken)
        {
            var existing = this._DbContext.FinancialStatement.Where(x => x.Id == request.Id).FirstOrDefault();

            if (existing != null)
            {
                existing.DeleteDate = DateTime.Now;

                this._DbContext.FinancialStatement.Update(existing);
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
