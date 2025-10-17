using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Create
{
    public class CreateProjectHRDailyReportCommand:IRequest<Result<bool>>
    {
        public int ProjectId { get; set; }
        public string? Expertise { get; set; }
        public bool IsDirect { get; set; }
        public string? Shift1 { get; set; }
        public string? Shift2 { get; set; }
        public string? Shift3 { get; set; }
        public decimal? Total { get; set; }
        public string? SubContractorName { get; set; }
    }

    public class CreateProjectHRDailyReportCommandHandler : IRequestHandler<CreateProjectHRDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public CreateProjectHRDailyReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(CreateProjectHRDailyReportCommand request, CancellationToken cancellationToken)
        {
            this._context.ProjectManpowerDailyReports.Add(new Domain.Entities.ProjectManpowerDailyReport() { 
                ProjectId = request.ProjectId,
                Created = DateTime.Now,
                Expertise = request.Expertise,
                IsDirect = request.IsDirect,
                Shift1 = request.Shift1,
                Shift2 = request.Shift2,
                Shift3 = request.Shift3,
                Total = request.Total,
                SubContractorName = request.SubContractorName
            });

            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
