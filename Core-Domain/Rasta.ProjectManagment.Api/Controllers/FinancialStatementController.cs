using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.FinancialStatement.Commands.Create;
using Rasta.ProjectManagment.Application.FinancialStatement.Commands.Delete;
using Rasta.ProjectManagment.Application.FinancialStatement.Commands.Update;
using Rasta.ProjectManagment.Application.FinancialStatement.Queries;
using Rasta.ProjectManagment.Application.FinancialStatement.Queries.GetByProject;

namespace Rasta.ProjectManagment.Api.Controllers
{
    public class FinancialStatementController : BaseApiController
    {
        [HttpPost]
        public async ValueTask<IActionResult> Create(CreateFinancialStatementCommand command, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async ValueTask<IActionResult> Update(UpdateFinancialStatementCommand command, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        public async ValueTask<IActionResult> Delete([FromQuery] DeleteFinancialStatementCommand command, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Read(GetByProjectQuery query, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(query);

            return Ok(result);
        }
    }
}
