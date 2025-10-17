using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events.ProjectProblemDailyReport;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Create;

public record CreateProjectProblemDailyReportCommand : IRequest<Result<long>>
{
    public required int ProjectId { get; set; }
    public required string ProblemsClassifications { get; set; }
    public required string Description { get; set; }

    public static implicit operator Domain.Entities.ProjectProblemDailyReport(CreateProjectProblemDailyReportCommand create)
    {
        return new Domain.Entities.ProjectProblemDailyReport
        {
            ProjectId = create.ProjectId,
            Description = create.Description,
            ProblemsClassifications = create.ProblemsClassifications
        };
    }
}

public class CreateProjectProblemDailyReportCommandHandler : IRequestHandler<CreateProjectProblemDailyReportCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "درج گزارش مشکلات";
    public CreateProjectProblemDailyReportCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<long>> Handle(CreateProjectProblemDailyReportCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = (Domain.Entities.ProjectProblemDailyReport)request;
        entity.AddDomainEvent(new ProjectProblemDailyReportCreatedEvent(entity));
        _context.ProjectProblemDailyReports.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<long>.Success(message, entity.Id));
    }
}