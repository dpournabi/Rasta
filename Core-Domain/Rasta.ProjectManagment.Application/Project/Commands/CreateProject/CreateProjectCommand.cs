using MediatR;
using Rasta.ProjectManagment.Domain.Enums;
using Rasta.ProjectManagment.Domain.Events;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Domain.Events.ProjectWorkBreakdown;

namespace Rasta.ProjectManagment.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<Result<long>>
    {
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

        public static implicit operator Domain.Entities.Project(CreateProjectCommand create)
        {
            return new Domain.Entities.Project
            {
                EmployerName = create.EmployerName,
                EndContractDate = create.EndContractDate,
                StartContractDate = create.StartContractDate,
                ProjectName = create.ProjectName,
                FloorCount = create.FloorCount,
                InfrastructureArea = create.InfrastructureArea,
                ProjectTypeId = create.ProjectTypeId,
                UnitCount = create.UnitCount,
                IsDelete = false,
                Abbr = create.Abbr,
                LandscapeArea = create.LandscapeArea,
                BlockCount = create.BlockCount,
                CityId = create.CityId,
                CityName = create.CityName, 
                ContractAmount = create.ContractAmount,
                ContractBasisIndex = create.ContractBasisIndex,
                HasBuyPlan = create.HasBuyPlan,
                HasExecutionPlan = create.HasExecutionPlan,
                HasCostBudjet = create.HasCostBudjet,
                ProjectManager = create.ProjectManager,
                ProvinceId = create.ProvinceId,
                ProvinceName = create.ProvinceName,
                Supervisor = create.Supervisor
            };
        }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<long>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "درج پروژه";
        public CreateProjectCommandHandler(IApplicationDbContext context, IResourceManager resourceManager)
        {
            _context = context;
            _resourceManager = resourceManager;
        }

        public async Task<Result<long>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            var entity = (Domain.Entities.Project)request;
            entity.AddDomainEvent(new ProjectCreatedEvent(entity));
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
            return await Task.FromResult(Result<long>.Success(message, entity.Id));
        }
    }
}
