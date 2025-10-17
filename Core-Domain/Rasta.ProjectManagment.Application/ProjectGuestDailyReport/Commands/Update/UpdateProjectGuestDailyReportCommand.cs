using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectAccidentDailyReport;
using Rasta.ProjectManagment.Domain.Events.ProjectGuestDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Update
{
    public class UpdateProjectGuestDailyReportCommand : IRequest<Result<long>>
    {
        public required long Id { get; set; }
        public required string VisitorName { get; set; }
        public string? OrganizationName { get; set; }
        public required DateTime EnterTime { get; set; }
        public required DateTime ExitTime { get; set; }
    }
    public class UpdateProjectAccidentDailyReportCommandHandler : IRequestHandler<UpdateProjectGuestDailyReportCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "به روزرسانی اطلاعات گزارش روزانه";

        public UpdateProjectAccidentDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<long>> Handle(UpdateProjectGuestDailyReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = string.Empty;
                var entity = await _context.ProjectGuestDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                    return await Task.FromResult(Result<long>.Failure(message, null, -1));
                }

                entity.VisitorName = request.VisitorName;
                entity.OrganizationName = request.OrganizationName;
                entity.EnterTime = request.EnterTime;
                entity.ExitTime = request.ExitTime;

                entity.AddDomainEvent(new ProjectGuestDailyReportUpdatedEvent(entity));
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
