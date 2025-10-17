using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.BasicInformationProperty;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.DeleteBasicInformationProperty;
public record DeleteBasicInformationPropertyCommand(int Id) : IRequest<Result<bool>>;
public class DeleteBasicInformationPropertyCommandHandler : IRequestHandler<DeleteBasicInformationPropertyCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "حذف اطلاعات پایه";

    public DeleteBasicInformationPropertyCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<bool>> Handle(DeleteBasicInformationPropertyCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.BasicInformationProperties.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        _context.BasicInformationProperties.Remove(entity);
        entity.AddDomainEvent(new BasicInformationPropertyDeletedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}
