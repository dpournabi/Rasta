using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Mappings;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.ProjectType.Queries.GetProjectTypeWithPagination;
using Rasta.ProjectManagment.Application.ProjectType.Queries;

namespace Rasta.ProjectManagment.Application.LookUp.Queries;
public class LookUpVM : IMapFrom<Domain.Entities.LookUp>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required string Type { get; set; }
}

