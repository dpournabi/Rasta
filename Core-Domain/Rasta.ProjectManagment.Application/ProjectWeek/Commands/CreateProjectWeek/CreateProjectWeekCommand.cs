using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.ProjectBreakdownHistory;

namespace Rasta.ProjectManagment.Application.ProjectWeek.Commands.CreateProjectWeek;
public class CreateProjectWeekCommand : IRequest<Result<long>>
{
    public required int ProjectId { get; set; }
    public required int WeekCount { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public static implicit operator Domain.Entities.ProjectWeek(CreateProjectWeekCommand create)
    {
        return new Domain.Entities.ProjectWeek
        {
            ProjectId = create.ProjectId,
            WeekCount = create.WeekCount,
            StartDate = create.StartDate,
            EndDate = create.EndDate,
            IsPlan = false,
            IsDelete=false
        };
    }
}
public class CreateProjectWeekCommanddHandler : IRequestHandler<CreateProjectWeekCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "درج مشخصات پروژه";

    public CreateProjectWeekCommanddHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<long>> Handle(CreateProjectWeekCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = (Domain.Entities.ProjectWeek)request;
        entity.AddDomainEvent(new ProjectBreakdownHistoryCreatedEvent(entity));
        _context.ProjectWeekes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<long>.Success(message, entity.Id));
    }
}
