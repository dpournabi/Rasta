using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.LookUp.Commands.CreateLookUp;
using Rasta.ProjectManagment.Application.LookUp.Commands.DeleteLookUp;
using Rasta.ProjectManagment.Application.LookUp.Commands.UpdateLookUp;
using Rasta.ProjectManagment.Application.LookUp.Queries;
using Rasta.ProjectManagment.Application.LookUp.Queries.GetLookUpWithPagination;

namespace Rasta.ProjectManagment.Api.Controllers;
public class LookUpController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<LookUpVM>>> Get([FromQuery] GetLookUpQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateLookUpCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateLookUpCommand command)
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
        await Mediator.Send(new DeleteLookUpCommand(id));

        return NoContent();
    }
}
