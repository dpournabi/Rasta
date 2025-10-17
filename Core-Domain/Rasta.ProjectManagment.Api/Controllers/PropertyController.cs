using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Property.Commands.CreateProperty;
using Rasta.ProjectManagment.Application.Property.Commands.DeleteProperty;
using Rasta.ProjectManagment.Application.Property.Commands.UpdateProperty;
using Rasta.ProjectManagment.Application.Property.Queries;
using Rasta.ProjectManagment.Application.Property.Queries.GetPropertyWithPagination;

namespace Rasta.ProjectManagment.Api.Controllers;
public class PropertyController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<PropertyVM>>> Get([FromQuery] GetPropertyWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreatePropertyCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(long id, UpdatePropertyCommand command)
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
        await Mediator.Send(new DeletePropertyCommand(id));

        return NoContent();
    }
}
