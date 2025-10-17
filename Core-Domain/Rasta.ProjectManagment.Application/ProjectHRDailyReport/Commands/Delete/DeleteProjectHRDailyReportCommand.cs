using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Delete
{
    public class DeleteProjectHRDailyReportCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteProjectHRDailyReportCommandHandler : IRequestHandler<DeleteProjectHRDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public DeleteProjectHRDailyReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteProjectHRDailyReportCommand request, CancellationToken cancellationToken)
        {
            var existing = this._context.ProjectManpowerDailyReports.FirstOrDefault(x => x.Id == request.Id);
            if (existing == null)
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }

            this._context.ProjectManpowerDailyReports.Remove(existing);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
