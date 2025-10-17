using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using org.apache.logging.log4j.util;
using org.apache.poi.poifs.property;
using org.sqlite.core;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Entities;
using System.Text;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSummaryLevel3
{
    public class GetSummaryLevel3Query : IRequest<Result<List<SP_GetSummaryLevel3VM>>>
    {
        public long ProjectId { get; set; }
        public int WeekNumber { get; set; }
    }

    public class GetSummaryLevel3QueryHandler : IRequestHandler<GetSummaryLevel3Query, Result<List<SP_GetSummaryLevel3VM>>>
    {
        private readonly IApplicationDbContext _context;

        public GetSummaryLevel3QueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<SP_GetSummaryLevel3VM>>> Handle(GetSummaryLevel3Query request, CancellationToken cancellationToken)
        {
            var validator = new GetSummaryLevel3QueryValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append("پارامترهای ورودی صحیح نمی باشند");
                foreach (var failure in validation.Errors)
                {
                    stringBuilder.Append($"Property: {failure.PropertyName} Error Code: {failure.ErrorCode}");
                }
                throw new FluentValidation.ValidationException(stringBuilder.ToString());
            }


            var parameterProjectId = new SqlParameter
            {
                ParameterName = "ProjectId",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Value = request.ProjectId,
            };

            var parameterWeekCount = new SqlParameter
            {
                ParameterName = "WeekCount",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = request.WeekNumber,
            };

            var result = await _context.SP_GetSummaryLevel3VMs
                                        .FromSqlRaw("[dbo].[GetSummaryLevel] @ProjectId, @WeekCount",
                                                                    parameterProjectId, parameterWeekCount)
                                        .ToListAsync();


            return Result<List<SP_GetSummaryLevel3VM>>.Success(string.Empty, await Task.FromResult(result));
        }
    }
}
