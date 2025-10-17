using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Update
{
    public class UpdateProjectExecutionDailyReportCommand : IRequest<Result<long>>
    {
        public required long Id { get; set; }
        public string? ZoneNo { get; set; }
        public string? BlockNo { get; set; }
        public string? MainOperation { get; set; }
        public string? SubOperation { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int TotalWorkTime { get; set; }//کارکرد
        public int TotalCumulativeWorkTime { get; set; }//کارکرد تجمعی
        public string? Unit { get; set; }//واحد
        public string? SubContractorName { get; set; }
        public int ActivityTime { get; set; }
        public string? Persons { get; set; }
    }
    public class UpdateProjectExecutionDailyReportCommandHandler : IRequestHandler<UpdateProjectExecutionDailyReportCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "به روزرسانی اطلاعات گزارش روزانه";

        public UpdateProjectExecutionDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<long>> Handle(UpdateProjectExecutionDailyReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = string.Empty;
                var entity = await _context.ProjectExecutionDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                    return await Task.FromResult(Result<long>.Failure(message, null, -1));
                }

                entity.Location = request.Location;
                entity.BlockNo = request.BlockNo;
                entity.MainOperation = request.MainOperation;
                entity.ZoneNo = request.ZoneNo;
                entity.ActivityTime = request.ActivityTime;
                entity.Description = request.Description;
                entity.Persons = request.Persons;
                entity.SubContractorName = request.SubContractorName;
                entity.SubOperation = request.SubOperation;
                entity.TotalCumulativeWorkTime = request.TotalCumulativeWorkTime;
                entity.TotalWorkTime = request.TotalWorkTime;
                entity.Unit = request.Unit;

                entity.AddDomainEvent(new ProjectDailyReportUpdatedEvent(entity));
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
