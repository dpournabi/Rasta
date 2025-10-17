using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.BasicInformation.Commands.CreateBasicInformation;
using Rasta.ProjectManagment.Application.BasicInformation.Commands.DeleteBasicInformation;
using Rasta.ProjectManagment.Application.BasicInformation.Commands.UpdateBasicInformation;
using Rasta.ProjectManagment.Application.BasicInformation.Queries;
using Rasta.ProjectManagment.Application.BasicInformation.Queries.GetBasicInformationWithPagination;
using Rasta.ProjectManagment.Application.BasicInformationProperty.Queries.GetBasicInformationPropertyWithPagination;
using Rasta.ProjectManagment.Application.BasicInformationProperty.Queries;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.CreateBasicInformationProperty;
using Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.DeleteBasicInformationProperty;
using Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.UpdateBasicInformationProperty;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Api.Models;

namespace Rasta.ProjectManagment.Api.Controllers;
public class BasicInformationController : BaseApiController
{
    public IDateTimeService DateTimeService { get; }

    public BasicInformationController(IDateTimeService _DateTimeService)
    {
        DateTimeService = _DateTimeService;
    }

    [HttpGet]
    public async Task<ActionResult<BasicInformationVM>> Get([FromQuery] GetBasicInformationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<int>>> Create([FromBody]CreateBasicInformationCommand command) => await Mediator.Send(command);

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<bool>>> Update(long id, UpdateBasicInformationCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<bool>>> Delete(int id) => await Mediator.Send(new DeleteBasicInformationCommand(id));

    #region BasicInformationProperty

    [HttpGet]
    public async Task<ActionResult<PaginatedList<BasicInformationPropertyVM>>> GetBasicInformationProperty([FromQuery] GetBasicInformationPropertyQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<int>> CreateBasicInformationProperty([FromBody]CreateBasicInformationPropertyCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<bool>>> UpdateBasicInformationProperty(long id, UpdateBasicInformationPropertyCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<bool>>> DeleteBasicInformationProperty(int id)=> await Mediator.Send(new DeleteBasicInformationPropertyCommand(id));
    #endregion

    [HttpGet]
    [ProducesResponseType(typeof(DateTimeResponseModel), 200)]
    public async Task<IActionResult> GetServerDate()
    {
        DateTime currentDate = DateTime.Now;
        string currentJalali = this.DateTimeService.GetPersianDateString(currentDate);
        return Ok(new DateTimeResponseModel() { 
            GregorianDate = currentDate,
            JalaliDate= currentJalali,
            GregorianYear = currentDate.Year,
            GregorianMonth = currentDate.Month,
            GregorianDay = currentDate.Day,
            JalaliYear = this.DateTimeService.GetPersianYear(currentDate),
            JalaliMonth = this.DateTimeService.GetPersianMonth(currentDate),
            JalaliDay = this.DateTimeService.GetPersianDay(currentDate)
        });
    }
}
