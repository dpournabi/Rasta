using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.MeasureUnit.Commands.CreateMeasureUnit;
using Rasta.ProjectManagment.Application.MeasureUnit.Commands.DeleteMeasureUnit;
using Rasta.ProjectManagment.Application.MeasureUnit.Commands.UpdateMeasureUnit;
using Rasta.ProjectManagment.Application.MeasureUnit.Queries;
using Rasta.ProjectManagment.Application.MeasureUnit.Queries.GetMeasureUnitWithPagination;

namespace Rasta.ProjectManagment.Api.Controllers;
public class MeasureUnitController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<MeasureUnitVM>>> Get([FromQuery] GetMeasureUnitQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateMeasureUnitCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateMeasureUnitCommand command)
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
        await Mediator.Send(new DeleteMeasureUnitCommand(id));

        return NoContent();
    }
}
