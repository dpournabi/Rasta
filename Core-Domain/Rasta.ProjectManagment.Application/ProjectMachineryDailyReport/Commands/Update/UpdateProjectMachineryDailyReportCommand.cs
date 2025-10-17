using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectMachineryDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Update
{
    public class UpdateProjectMachineryDailyReportCommand : IRequest<Result<long>>
    {
        public required long Id { get; set; }
        public required string MachineryEquipmentDescription { get; set; }
        public required int WorkingHours { get; set; }
        public required bool IsActive { get; set; }
        public required bool NeedRepair { get; set; }
        public decimal? Total { get; set; }
        public string? Ownership { get; set; }
    }
    public class UpdateProjectMachineryDailyReportCommandHandler : IRequestHandler<UpdateProjectMachineryDailyReportCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "به روزرسانی اطلاعات گزارش روزانه";

        public UpdateProjectMachineryDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<long>> Handle(UpdateProjectMachineryDailyReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = string.Empty;
                var entity = await _context.ProjectMachineryDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                    return await Task.FromResult(Result<long>.Failure(message, null, -1));
                }

                entity.MachineryEquipmentDescription = request.MachineryEquipmentDescription;
                entity.IsActive = request.IsActive;
                entity.NeedRepair = request.NeedRepair;
                entity.WorkingHours = request.WorkingHours;
                entity.Total = request.Total;
                entity.Ownership = request.Ownership;
                
                entity.AddDomainEvent(new ProjectMachineryDailyReportUpdatedEvent(entity));
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
