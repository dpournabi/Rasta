using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Commands.UpdateACProjectWorkBreakdownHistoryWork
{
    public class UpdateACProjectWorkBreakdownHistoryCommand : IRequest<Result<Domain.Entities.ProjectWorkBreakdownHistoryWork>>
    {
        public long Id { get; set; }
        public double? RealPercentage { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }
    }
    public class UpdateACProjectWorkBreakdownHistoryCommandHandler : IRequestHandler<UpdateACProjectWorkBreakdownHistoryCommand, Result<Domain.Entities.ProjectWorkBreakdownHistoryWork>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "ثبت گزارشات هفتگی";
        public UpdateACProjectWorkBreakdownHistoryCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }

        public async Task<Result<Domain.Entities.ProjectWorkBreakdownHistoryWork>> Handle(UpdateACProjectWorkBreakdownHistoryCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            var entity = await _context.ProjectWorkBreakdownHistoryWorks
                                       .Include(x=>x.ProjectWeek)
                                       .Include(x=>x.ProjectWorkBreakdown)
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                return await Task.FromResult(Result<Domain.Entities.ProjectWorkBreakdownHistoryWork>.Failure(message, null, null));
            }

            entity.RealPercentage = request.RealPercentage;
            entity.Description = request.Description;
            entity.IsDone = request.IsDone;
            entity = await CalculateCurrentWeekACAsync(request.RealPercentage, entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
            return await Task.FromResult(Result<Domain.Entities.ProjectWorkBreakdownHistoryWork>.Success(message, entity));
        }

        private async Task<Domain.Entities.ProjectWorkBreakdownHistoryWork> CalculateCurrentWeekACAsync(double? RealPercentage, Domain.Entities.ProjectWorkBreakdownHistoryWork entity, CancellationToken cancellationToken)
        {
            var pwbHistoryWorks = await (from pwb in _context.ProjectWorkBreakdownHistoryWorks
                                         join pw in _context.ProjectWeekes on pwb.Id equals pw.Id
                                         where pwb.ProjectWorkBreakdownId == entity.ProjectWorkBreakdownId &&
                                               pw.WeekCount < entity.ProjectWeek.WeekCount
                                         orderby pw.WeekCount
                                         select pwb)
                                  .ToListAsync(cancellationToken);

            var sumRealPercent = pwbHistoryWorks.Sum(p => p.RealPercentage);
            var ratio = 100 - sumRealPercent;
            var cumulativeRealRatio = sumRealPercent + (entity.RealPercentage * ratio);

            entity.RealPercentage = ratio * RealPercentage;
            entity.RealCumulativePercentage = cumulativeRealRatio;
            entity.ActualCompleteProgressPercentage = ratio * entity.RealPercentage * entity?.ProjectWorkBreakdown?.WeightFactor;
            entity.ActualWeightProgressPercentage = cumulativeRealRatio * entity?.ProjectWorkBreakdown?.WeightFactor;

            return await Task.FromResult(entity);
        }
    }
}
