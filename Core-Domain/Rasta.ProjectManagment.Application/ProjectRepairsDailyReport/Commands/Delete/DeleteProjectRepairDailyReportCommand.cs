using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Delete
{
    public class DeleteProjectRepairDailyReportCommand: IRequest<Result<bool>>
    {
        public required long Id { get; set; }
    }

    public class DeleteProjectRepairDailyReportCommandHandler :
        IRequestHandler<DeleteProjectRepairDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;

        public DeleteProjectRepairDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }

        public async Task<Result<bool>> Handle(DeleteProjectRepairDailyReportCommand request, CancellationToken cancellationToken)
        {
            var existing = this._context.ProjectRepairDailyReports.FirstOrDefault(r => r.Id == request.Id);
            if (existing == null)
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }

            this._context.ProjectRepairDailyReports.Remove(existing);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
