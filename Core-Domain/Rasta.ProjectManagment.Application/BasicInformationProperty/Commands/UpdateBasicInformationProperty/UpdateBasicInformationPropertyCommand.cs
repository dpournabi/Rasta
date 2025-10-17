using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.BasicInformationProperty;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.UpdateBasicInformationProperty;
public class UpdateBasicInformationPropertyCommand : IRequest<Result<bool>>
{
    public required int Id { get; set; }
    public required int BasicInformationId { get; set; }
    public required int PropertyId { get; set; }
    public required string Value { get; set; }
}

public class UpdateBasicInformationPropertyCommandHandler : IRequestHandler<UpdateBasicInformationPropertyCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "به روزرسانی اطلاعات پایه";

    public UpdateBasicInformationPropertyCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<bool>> Handle(UpdateBasicInformationPropertyCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.BasicInformationProperties.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        entity.BasicInformationId = request.BasicInformationId;
        entity.PropertyId = request.PropertyId;
        entity.Value = request.Value;
        entity.AddDomainEvent(new BasicInformationPropertyUpdatedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));

    }
}
