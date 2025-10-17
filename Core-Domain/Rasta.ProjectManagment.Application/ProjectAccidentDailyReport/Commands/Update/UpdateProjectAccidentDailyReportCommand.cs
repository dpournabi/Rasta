using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Update
{
    public class UpdateProjectAccidentDailyReportCommand : IRequest<Result<long>>
    {
        public required long Id { get; set; }
        public required string Reason { get; set; }
        public required int AccidentTypeId { get; set; }
        public string? AccidentEffect { get; set; }
        public int? DaysLostCount { get; set; }
        public decimal? DamageAmount { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateProjectAccidentDailyReportCommandHandler : IRequestHandler<UpdateProjectAccidentDailyReportCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "به روزرسانی اطلاعات گزارش حادثه";

        public UpdateProjectAccidentDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<long>> Handle(UpdateProjectAccidentDailyReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = string.Empty;
                var entity = await _context.ProjectAccidentDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                    return await Task.FromResult(Result<long>.Failure(message, null, -1));
                }

                entity.Reason = request.Reason;
                entity.AccidentEffect = request.AccidentEffect;
                entity.AccidentTypeId = request.AccidentTypeId;
                entity.DamageAmount = request.DamageAmount;
                entity.DaysLostCount = request.DaysLostCount;
                entity.Description = request.Description;

                entity.AddDomainEvent(new ProjectAccidentDailyReportUpdatedEvent(entity));
                await _context.SaveChangesAsync(cancellationToken);

                message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
                return await Task.FromResult(Result<long>.Success(message, entity.Id));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result<long>.Failure(null, new string[] { ex.InnerException != null ? ex.InnerException.Message : ex.Message.ToString() }, -1));
            }
        }
    }
}
