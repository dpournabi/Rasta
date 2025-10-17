//using Microsoft.AspNetCore.Mvc;
//using Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.CreateWorkBreakdownStructure;
//using Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.DeleteWorkBreakdownStructure;
//using Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.UpdateWorkBreakdownStructure;
//using Rasta.ProjectManagment.Application.WorkBreakdownStructure.Queries;
//using Rasta.ProjectManagment.Application.WorkBreakdownStructure.Queries.GetWorkBreakdown;

//namespace Rasta.ProjectManagment.Api.Controllers;
//public class WorkBreakdownStructureController : BaseApiController
//{
//    [HttpGet]
//    public async Task<ActionResult<WorkBreakdownStructureVM>> Get([FromQuery] GetWorkBreakownQuery query)
//    {
//        var result = await Mediator.Send(query);
//        return result;
//    }

//    [HttpPost]
//    public async Task<ActionResult<long>> Create([FromBody] CreateWorkBreakdownStructureCommand command)
//    {
//        return await Mediator.Send(command);
//    }

//    [HttpPut("{id}")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesDefaultResponseType]
//    public async Task<IActionResult> Update(long id, UpdateWorkBreakdownStructureCommand command)
//    {
//        if (id != command.Id)
//        {
//            return BadRequest();
//        }

//        await Mediator.Send(command);

//        return NoContent();
//    }

//    [HttpDelete("{id}")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesDefaultResponseType]
//    public async Task<IActionResult> Delete(int id)
//    {
//        await Mediator.Send(new DeleteWorkBreakdownStructureCommand(id));

//        return NoContent();
//    }

//}
