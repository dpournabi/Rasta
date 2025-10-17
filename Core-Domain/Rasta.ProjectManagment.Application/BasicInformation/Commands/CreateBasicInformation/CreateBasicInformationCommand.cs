using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.BasicInformation;

namespace Rasta.ProjectManagment.Application.BasicInformation.Commands.CreateBasicInformation;
public class CreateBasicInformationCommand : IRequest<Result<int>>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required int Level { get; set; }
    public int? MeasureUnitId { get; set; }
    public int? ParentId { get; set; }

    public static implicit operator Domain.Entities.BasicInformation(CreateBasicInformationCommand create)
    {
        return new Domain.Entities.BasicInformation
        {
            Code = create.Code,
            Name = create.Name,
            Level = create.Level,
            MeasureUnitId = create.MeasureUnitId,
            ParentId = create.ParentId
        };
    }
}

public class CreateBasicInformationCommandHandler : IRequestHandler<CreateBasicInformationCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "درج اطلاعات پایه";

    public CreateBasicInformationCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<int>> Handle(CreateBasicInformationCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = (Domain.Entities.BasicInformation)request;
        entity.AddDomainEvent(new BasicInformationCreatedEvent(entity));
        _context.BasicInformations.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<int>.Success(message, entity.Id));
    }
}
