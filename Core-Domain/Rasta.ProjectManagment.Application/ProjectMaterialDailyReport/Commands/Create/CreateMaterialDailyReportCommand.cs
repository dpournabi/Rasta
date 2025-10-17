using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Create
{
    public class CreateMaterialDailyReportCommand: IRequest<Result<bool>>
    {
        public required int ProjectId { get; set; }
        public required string MaterialsDescription { get; set; }
        public required int UnitId { get; set; }
        public required double ImportAmount { get; set; }
        public string? UsePlace { get; set; }
    }

    public class CreateMaterialDailyReportCommandHandler : IRequestHandler<CreateMaterialDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public CreateMaterialDailyReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(CreateMaterialDailyReportCommand request, CancellationToken cancellationToken)
        {
            this._context.ProjectMaterialsDailyReports.Add(new Domain.Entities.ProjectMaterialsDailyReport() { 
                Created = DateTime.Now,
                ImportAmount = request.ImportAmount,
                MaterialsDescription = request.MaterialsDescription,
                ProjectId = request.ProjectId,
                UnitId = request.UnitId,
                UsePlace = request.UsePlace
            });

            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
