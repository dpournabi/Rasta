using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Update
{
    public class UpdateProjectProblemDailyReportCommand : IRequest<Result<long>>
    {
        public required long Id { get; set; }
        public required string ProblemsClassifications { get; set; }
        public required string Description { get; set; }
    }
    public class UpdateProjectProblemDailyReportCommandHandler : IRequestHandler<UpdateProjectProblemDailyReportCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "به روزرسانی اطلاعات گزارش مشکلات";

        public UpdateProjectProblemDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<long>> Handle(UpdateProjectProblemDailyReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = string.Empty;
                var entity = await _context.ProjectProblemDailyReports.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                    return await Task.FromResult(Result<long>.Failure(message, null, -1));
                }

                entity.Description = request.Description;
                entity.ProblemsClassifications = request.ProblemsClassifications;

                entity.AddDomainEvent(new ProjectProblemDailyReportUpdatedEvent(entity));
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
