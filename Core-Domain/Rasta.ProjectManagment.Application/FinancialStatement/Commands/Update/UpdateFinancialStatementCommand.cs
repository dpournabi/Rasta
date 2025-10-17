using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.FinancialStatement.Commands.Update
{
    public class UpdateFinancialStatementCommand : Domain.Entities.FinancialStatement, IRequest<Result<bool>>
    {

    }

    public class UpdateFinancialStatementCommandHandler : IRequestHandler<UpdateFinancialStatementCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _DbContext;
        public UpdateFinancialStatementCommandHandler(IApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<Result<bool>> Handle(UpdateFinancialStatementCommand request, CancellationToken cancellationToken)
        {
            var existing = this._DbContext.FinancialStatement.Where(x => x.Id == request.Id).FirstOrDefault();

            if (existing != null)
            {
                existing.IntegratedSent = request.IntegratedSent;
                existing.PeriodSent = request.PeriodSent;
                existing.SentDate = request.SentDate;
                existing.IntegratedApproved = request.IntegratedApproved;
                existing.PeriodApproved = request.PeriodApproved;
                existing.ApprovedDate = request.ApprovedDate;
                existing.BalancedIntegratedApproved= request.BalancedIntegratedApproved;
                existing.BalancedPeriodApproved= request.BalancedPeriodApproved;
                existing.BalancedDate= request.BalancedDate;                

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
