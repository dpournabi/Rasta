using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.BasicInformation;

namespace Rasta.ProjectManagment.Application.BasicInformation.Commands.DeleteBasicInformation;
public record DeleteBasicInformationCommand(int Id) : IRequest<Result<bool>>;
public class DeleteBasicInformationCommandHandler : IRequestHandler<DeleteBasicInformationCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IResourceManager _resourceManager;
    private const string _Operation = "حذف اطلاعات پایه";

    public DeleteBasicInformationCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
    {
        _context = context;
        _resourceManager = resourceManager;
    }

    public async Task<Result<bool>> Handle(DeleteBasicInformationCommand request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        var entity = await _context.BasicInformations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
            return await Task.FromResult(Result<bool>.Failure(message, null, false));
        }

        _context.BasicInformations.Remove(entity);
        entity.AddDomainEvent(new BasicInformationDeletedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
        return await Task.FromResult(Result<bool>.Success(message, true));
    }
}

