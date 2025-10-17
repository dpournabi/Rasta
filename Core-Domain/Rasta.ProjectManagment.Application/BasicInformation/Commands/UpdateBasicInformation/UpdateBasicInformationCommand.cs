using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.BasicInformation;

namespace Rasta.ProjectManagment.Application.BasicInformation.Commands.UpdateBasicInformation;
public class UpdateBasicInformationCommand : IRequest<Result<bool>>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required int Level { get; set; }
    public int? MeasureUnitId { get; set; }
}

public class UpdateBasicInformationCommandHandler : IRequestHandler<UpdateBasicInformationCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "به روزرسانی اطلاعات پایه";

    public UpdateBasicInformationCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<bool>> Handle(UpdateBasicInformationCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.BasicInformations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.Level = request.Level;
        entity.MeasureUnitId = request.MeasureUnitId;
        entity.AddDomainEvent(new BasicInformationUpdatedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);

        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}
