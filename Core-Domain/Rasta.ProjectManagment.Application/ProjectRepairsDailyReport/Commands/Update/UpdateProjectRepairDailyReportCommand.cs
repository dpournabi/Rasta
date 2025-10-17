using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Update
{
    public class UpdateProjectRepairDailyReportCommand: IRequest<Result<bool>>
    {
        public required long Id { get; set; }
        public required string Title { get; set; }
        public bool RepairStatus { get; set; }
        public required string RepairPlace { get; set; }
        public decimal? EstimateInitialCost { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateProjectRepairDailyReportCommandHandler : IRequestHandler<UpdateProjectRepairDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        public UpdateProjectRepairDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<bool>> Handle(UpdateProjectRepairDailyReportCommand request, CancellationToken cancellationToken)
        {
            var existing = this._context.ProjectRepairDailyReports.FirstOrDefault(r => r.Id == request.Id);
            if (existing == null)
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }

            existing.Description = request.Description;
            existing.EstimateInitialCost = request.EstimateInitialCost;
            existing.LastModified = DateTime.Now;
            existing.RepairPlace = request.RepairPlace;
            existing.RepairStatus = request.RepairStatus;
            existing.Title= request.Title;

            this._context.ProjectRepairDailyReports.Update(existing);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
