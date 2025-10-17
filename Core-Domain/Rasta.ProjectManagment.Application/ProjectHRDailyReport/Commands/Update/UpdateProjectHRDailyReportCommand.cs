using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Update
{
    public class UpdateProjectHRDailyReportCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
        public string? Expertise { get; set; }
        public bool IsDirect { get; set; }
        public string? Shift1 { get; set; }
        public string? Shift2 { get; set; }
        public string? Shift3 { get; set; }
        public decimal? Total { get; set; }
        public string? SubContractorName { get; set; }
    }

    public class UpdateHRDailyReportCommandHandler : IRequestHandler<UpdateProjectHRDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public UpdateHRDailyReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(UpdateProjectHRDailyReportCommand request, CancellationToken cancellationToken)
        {
            var existing = this._context.ProjectManpowerDailyReports.FirstOrDefault(x => x.Id == request.Id);
            if (existing == null)
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }

            existing.Expertise = request.Expertise;
            existing.IsDirect = request.IsDirect;
            existing.Shift1 = request.Shift1;
            existing.Shift2 = request.Shift2;
            existing.Shift3 = request.Shift3;
            existing.Total = request.Total;
            existing.SubContractorName= request.SubContractorName;

            this._context.ProjectManpowerDailyReports.Update(existing);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
