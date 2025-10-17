using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Delete
{
    public class DeleteMaterialDailyReportCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteMaterialDailyReportCommandHandler : IRequestHandler<DeleteMaterialDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public DeleteMaterialDailyReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteMaterialDailyReportCommand request, CancellationToken cancellationToken)
        {
            var existing = this._context.ProjectMaterialsDailyReports.FirstOrDefault(x => x.Id == request.Id);
            if (existing == null)
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }

            this._context.ProjectMaterialsDailyReports.Remove(existing);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
