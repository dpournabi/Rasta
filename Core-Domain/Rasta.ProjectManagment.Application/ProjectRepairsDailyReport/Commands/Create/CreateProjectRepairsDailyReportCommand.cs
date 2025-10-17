using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Create
{
    public class CreateProjectRepairsDailyReportCommand: IRequest<Result<bool>>
    {
        public required int ProjectId { get; set; }
        public required string Title { get; set; }
        public bool RepairStatus { get; set; }
        public required string RepairPlace { get; set; }
        public decimal? EstimateInitialCost { get; set; }
        public string? Description { get; set; }

    }

    public class CreateProjectRepairsDailyReportCommandHandler : IRequestHandler<CreateProjectRepairsDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;

        public CreateProjectRepairsDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<bool>> Handle(CreateProjectRepairsDailyReportCommand request, CancellationToken cancellationToken)
        {
            this._context.ProjectRepairDailyReports.Add(new Domain.Entities.ProjectRepairDailyReport() { 
                Created= DateTime.Now,
                ProjectId= request.ProjectId,
                RepairPlace= request.RepairPlace,
                Title= request.Title,
                Description= request.Description,
                EstimateInitialCost= request.EstimateInitialCost,
                RepairStatus= request.RepairStatus,
            });

            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
