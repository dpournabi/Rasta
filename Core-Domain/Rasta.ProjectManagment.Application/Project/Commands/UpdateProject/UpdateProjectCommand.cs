using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Exceptions;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Domain.Events.Project;

namespace Rasta.ProjectManagment.Application.Project.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<Result<long>>
    {
        public required long Id { get; set; }
        public required string ProjectName { get; set; }
        public required int ProjectTypeId { get; set; }
        public required string EmployerName { get; set; }
        public required int InfrastructureArea { get; set; }
        public required int FloorCount { get; set; }
        public required int UnitCount { get; set; }
        public required DateTime StartContractDate { get; set; }
        public required DateTime EndContractDate { get; set; }
        public required string Abbr { get; set; }
        public int? ProvinceId { get; set; }
        public string? ProvinceName { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public string? ProjectManager { get; set; }
        public string? Supervisor { get; set; }
        public required int LandscapeArea { get; set; }
        public long? ContractBasisIndex { get; set; }
        public int? BlockCount { get; set; }
        public decimal? ContractAmount { get; set; }
        public bool? HasBuyPlan { get; set; }
        public bool? HasExecutionPlan { get; set; }
        public bool? HasCostBudjet { get; set; }

    }
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "به روزرسانی اطلاعات پروژه";

        public UpdateProjectCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }
        public async Task<Result<long>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = string.Empty;
                var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                    return await Task.FromResult(Result<long>.Failure(message, null, -1));
                }

                entity.EmployerName = request.EmployerName;
                entity.EndContractDate = request.EndContractDate;
                entity.StartContractDate = request.StartContractDate;
                entity.ProjectName = request.ProjectName;
                entity.FloorCount = request.FloorCount;
                entity.InfrastructureArea = request.InfrastructureArea;
                entity.ProjectTypeId = request.ProjectTypeId;
                entity.UnitCount = request.UnitCount;
                entity.IsDelete = false;
                entity.Abbr = request.Abbr;
                entity.LandscapeArea = request.LandscapeArea;
                entity.BlockCount = request.BlockCount;
                entity.CityId = request.CityId;
                entity.CityName = request.CityName;
                entity.ContractAmount = request.ContractAmount;
                entity.ContractBasisIndex = request.ContractBasisIndex;
                entity.HasBuyPlan = request.HasBuyPlan;
                entity.HasExecutionPlan = request.HasExecutionPlan;
                entity.HasCostBudjet = request.HasCostBudjet;
                entity.ProjectManager = request.ProjectManager;
                entity.ProvinceId = request.ProvinceId;
                entity.ProvinceName = request.ProvinceName;
                entity.Supervisor = request.Supervisor;

                entity.AddDomainEvent(new ProjectUpdatedEvent(entity));
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
