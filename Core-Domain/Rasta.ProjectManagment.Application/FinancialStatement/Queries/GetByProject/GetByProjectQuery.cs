using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.FinancialStatement.Queries.GetByProject
{
    public class GetByProjectQuery: IRequest<Result<PaginatedList<Domain.Entities.FinancialStatement>>>
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public int? Year { get; set; }
        public short? Month { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

        public GetByProjectQuery()
        {
            this.PageSize = 20;
            this.PageNumber = 1;
        }
    }

    public class GetByProjectQueryHandler: IRequestHandler<GetByProjectQuery, Result<PaginatedList<Domain.Entities.FinancialStatement>>>
    {
        private readonly IApplicationDbContext _DbContext;
        public GetByProjectQueryHandler(IApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<Result<PaginatedList<Domain.Entities.FinancialStatement>>> Handle(GetByProjectQuery request
            , CancellationToken cancellationToken)
        {
            var query = this._DbContext.FinancialStatement.AsQueryable().Where(f => !f.DeleteDate.HasValue);

            if (request.Id.HasValue)
            {
                query = query.Where(f => f.Id == request.Id);  
            }
            if (request.ProjectId.HasValue)
            {
                query = query.Where(f => f.ProjectId == request.ProjectId);
            }
            if(request.Year.HasValue)
            {
                query = query.Where(f => f.OperationYear == request.Year);
            }
            if (request.Month.HasValue)
            {
                query = query.Where(f => f.OperationMonth == request.Month);
            }
            if (request.FromDate.HasValue)
            {
                query = query.Where(c => c.CreateDate >= request.FromDate);
            }
            if (request.ToDate.HasValue)
            {
                request.ToDate = request.ToDate.Value.AddDays(1);
                query = query.Where(c => c.CreateDate <= request.ToDate);
            }

            var result = await query.PaginatedListAsync(request.PageNumber.Value, request.PageSize.Value);

            return Result<PaginatedList<Domain.Entities.FinancialStatement>>.Success("Ok", result);
        }
    }
}
