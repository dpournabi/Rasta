using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Create
{
    public class CreateProjectExecutionDailyReportCommand : IRequest<Result<long>>
    {
        public required int ProjectId { get; set; }
        public string? ZoneNo { get; set; }
        public string? BlockNo { get; set; }
        public string? MainOperation { get; set; }
        public string? SubOperation { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int TotalWorkTime { get; set; }//کارکرد
        public int TotalCumulativeWorkTime { get; set; }//کارکرد تجمعی
        public string? Unit { get; set; }//واحد
        public string? SubContractorName { get; set; }
        public int ActivityTime { get; set; }
        public string? Persons { get; set; }

        public static implicit operator Domain.Entities.ProjectExecutionDailyReport(CreateProjectExecutionDailyReportCommand create)
        {
            return new Domain.Entities.ProjectExecutionDailyReport
            {
                ProjectId = create.ProjectId,
                Persons = create.Persons,
                TotalCumulativeWorkTime = create.TotalCumulativeWorkTime,
                TotalWorkTime = create.TotalWorkTime,
                ZoneNo = create.ZoneNo,
                ActivityTime = create.ActivityTime,
                BlockNo = create.BlockNo,
                Description = create.Description,
                Location = create.Location,
                MainOperation = create.MainOperation,
                SubContractorName = create.SubContractorName,
                SubOperation = create.SubOperation,
                Unit = create.Unit
            };
        }
    }

    public class CreateProjectDailyReportCommandHandler : IRequestHandler<CreateProjectExecutionDailyReportCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "درج گزارش روزانه";
        public CreateProjectDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }

        public async Task<Result<long>> Handle(CreateProjectExecutionDailyReportCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            var entity = (Domain.Entities.ProjectExecutionDailyReport)request;
            entity.AddDomainEvent(new ProjectDailyReportCreatedEvent(entity));
            _context.ProjectExecutionDailyReports.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
            return await Task.FromResult(Result<long>.Success(message, entity.Id));
        }
    }
}