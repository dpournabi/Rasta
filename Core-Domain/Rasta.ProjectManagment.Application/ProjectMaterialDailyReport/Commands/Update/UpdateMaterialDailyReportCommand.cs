using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Update
{
    public class UpdateMaterialDailyReportCommand: IRequest<Result<bool>>
    {
        public required long Id { get; set; }
        public required string MaterialsDescription { get; set; }
        public required int UnitId { get; set; }
        public required double ImportAmount { get; set; }
        public string? UsePlace { get; set; }
    }

    public class UpdateMaterialDailyReportCommandHandler : IRequestHandler<UpdateMaterialDailyReportCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        public UpdateMaterialDailyReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>>  Handle(UpdateMaterialDailyReportCommand request, CancellationToken cancellationToken)
        {
            var existing = this._context.ProjectMaterialsDailyReports.FirstOrDefault(x => x.Id == request.Id);  
            if (existing == null)
            {
                return Result<bool>.Failure("Not Found!", null, false);
            }

            existing.ImportAmount = request.ImportAmount;
            existing.LastModified = DateTime.Now;
            existing.MaterialsDescription = request.MaterialsDescription;
            existing.UnitId = request.UnitId;
            existing.UsePlace = request.UsePlace;
            
            this._context.ProjectMaterialsDailyReports.Update(existing);
            await this._context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success("OK", true);
        }
    }
}
