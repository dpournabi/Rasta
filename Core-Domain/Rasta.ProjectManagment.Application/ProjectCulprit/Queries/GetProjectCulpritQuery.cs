using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectCulprit.Queries
{
    public class GetProjectCulpritQuery: IRequest<Result<PaginatedList<View_Culprits>>>
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public int? JobTitleId { get; set; }
        public int? WeekNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

        public GetProjectCulpritQuery()
        {
            this.PageSize = 20;
            this.PageNumber = 1;
        }
    }
    public class GetProjectCulpritQueryHandler : IRequestHandler<GetProjectCulpritQuery, Result<PaginatedList<View_Culprits>>>
    {
        private readonly IApplicationDbContext _DbContext;
        public GetProjectCulpritQueryHandler(IApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<Result<PaginatedList<View_Culprits>>> Handle(GetProjectCulpritQuery model, CancellationToken cancellationToken)
        {
            var query = this._DbContext.View_Culprits.AsQueryable();

            if (model.Id.HasValue)
            {
                query = query.Where(c => c.Id == model.Id);
            }
            if (model.ProjectId.HasValue)
            {
                query = query.Where(c => c.ProjectId == model.ProjectId);
            }
            if (model.JobTitleId.HasValue)
            {
                query = query.Where(c => c.JobTitleId == model.JobTitleId);
            }
            if (model.WeekNumber.HasValue)
            {
                query = query.Where(c => c.WeekNumber == model.WeekNumber);
            }
            if (model.FromDate.HasValue)
            {
                query = query.Where(c => c.CreateDate >= model.FromDate);
            }
            if (model.ToDate.HasValue)
            {
                model.ToDate = model.ToDate.Value.AddDays(1);
                query = query.Where(c => c.CreateDate <= model.ToDate);
            }

            var result = await query.PaginatedListAsync(model.PageNumber.Value, model.PageSize.Value);

            return Result<PaginatedList<View_Culprits>>.Success("Ok", result);
        }
    }
}
