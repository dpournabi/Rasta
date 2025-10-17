using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.ProjectCulprit.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectCulprit.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectCulprit.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectCulprit.Queries;

namespace Rasta.ProjectManagment.Api.Controllers
{
    public class ProjectCulpritController : BaseApiController
    {
        [HttpPost]
        public async ValueTask<IActionResult> Create(CreateProjectCulpritCommand command, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async ValueTask<IActionResult> Update(UpdateProjectCulpritCommand command, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        public async ValueTask<IActionResult> Delete([FromQuery]DeleteProjectCulpritCommand command, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Read(GetProjectCulpritQuery query, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(query);

            return Ok(result);
        }
    }
}
