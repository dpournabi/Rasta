using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.ProjectType.Commands.CreateProjectType;
using Rasta.ProjectManagment.Application.ProjectType.Commands.DeleteProjectType;
using Rasta.ProjectManagment.Application.ProjectType.Commands.UpdateProjectType;
using Rasta.ProjectManagment.Application.ProjectType.Queries;
using Rasta.ProjectManagment.Application.ProjectType.Queries.GetProjectTypeWithPagination;
using Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.CreateProjectTypeRatio;
using Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.DeleteProjectTypeRatio;
using Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.UpdateProjectTypeRatio;
using Rasta.ProjectManagment.Application.ProjectTypeRatio.Queries;
using Rasta.ProjectManagment.Application.ProjectTypeRatio.Queries.GetProjectTypeRatioWithPagination;

namespace Rasta.ProjectManagment.Api.Controllers
{
    public class ProjectTypeController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectTypeVM>>> Get([FromQuery] GetProjectTypeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateProjectTypeCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(int id, UpdateProjectTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProjectTypeCommand(id));

            return NoContent();
        }

        #region ProjectTyperatio
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectTypeRatioVM>>> GetProjectTypeRatio([FromQuery] GetProjectTypeRatioQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProjectTypeRatio([FromBody] CreateProjectTypeRatioCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateProjectTypeRatio(int id, UpdateProjectTypeRatioCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProjectTypeRatio(int id)
        {
            await Mediator.Send(new DeleteProjectTypeRatioCommand(id));

            return NoContent();
        }

        #endregion
    }
}
