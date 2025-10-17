using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Project.Queries.GetProjectWithPagination;

namespace Rasta.ProjectManagment.Application.Project.Queries.GetProjectByProjectNameQuery
{
    public record GetProjectWithPaginationQuery : IRequest<PaginatedList<ProjectBriefVM>>
    {
        public string? ProjectName { get; set; }
        public int? ProjectTypeId { get; set; }
        public string? EmployerName { get; set; }
        public int? InfrastructureArea { get; set; }
        public DateTime? StartContractDate { get; set; }
        public DateTime? EndContractDate { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetProjectWithPaginationQueryHandler : IRequestHandler<GetProjectWithPaginationQuery, PaginatedList<ProjectBriefVM>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProjectBriefVM>> Handle(GetProjectWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await BuildQuery(request).Include(x => x.ProjectType)
                //.ProjectTo<ProjectBriefVM>(_mapper.ConfigurationProvider, dest=>dest.pr)
                .Select(x => new ProjectBriefVM
                {
                    Abbr = x.Abbr,
                    EmployerName = x.EmployerName,
                    EndContractDate = x.EndContractDate,
                    FloorCount = x.FloorCount,
                    Id = x.Id,
                    InfrastructureArea = x.InfrastructureArea,
                    LandscapeArea = x.LandscapeArea,
                    ProjectName = x.ProjectName,
                    ProjectTypeId = x.ProjectTypeId,
                    ProjectTypeTitle = x.ProjectType.Title,
                    StartContractDate = x.StartContractDate,
                    UnitCount = x.UnitCount,
                    BlockCount = x.BlockCount,
                    CityId = x.CityId,
                    CityName = x.CityName,
                    ContractAmount = x.ContractAmount,
                    ContractBasisIndex = x.ContractBasisIndex,
                    HasBuyPlan = x.HasBuyPlan,
                    HasCostBudjet = x.HasCostBudjet,
                    HasExecutionPlan = x.HasExecutionPlan,
                    ProjectManager = x.ProjectManager,
                    ProvinceId = x.ProvinceId,
                    ProvinceName = x.ProvinceName,
                    Supervisor = x.Supervisor
                }).PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private IQueryable<Domain.Entities.Project> BuildQuery(GetProjectWithPaginationQuery request)
        {
            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.ProjectName))
                query = query.Where(x => x.ProjectName.Contains(request.ProjectName));

            if (!string.IsNullOrWhiteSpace(request.EmployerName))
                query = query.Where(x => x.EmployerName.Contains(request.EmployerName));

            if (request.InfrastructureArea is not null && request.InfrastructureArea > 0)
                query = query.Where(x => x.InfrastructureArea > request.InfrastructureArea);

            if (request.StartContractDate is not null)
                query = query.Where(x => x.StartContractDate >= request.StartContractDate);

            if (request.EndContractDate is not null)
                query = query.Where(x => x.EndContractDate <= request.EndContractDate);

            if(request.ProjectTypeId.HasValue)
            {
                query = query.Where(x => x.ProjectTypeId == request.ProjectTypeId.Value);
            }

            return query;
        }
    }
}
