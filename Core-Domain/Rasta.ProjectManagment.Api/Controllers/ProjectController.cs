#region Using
using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Application.Project.Commands.CreateProject;
using Rasta.ProjectManagment.Application.Project.Commands.UpdateProject;
using Rasta.ProjectManagment.Application.Project.Queries.GetProjectByProjectNameQuery;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistory.Queries.GetProjectWorkBreakdownHistoryWithPagination;
using Rasta.ProjectManagment.Application.ProjectWeek.Commands.CreateProjectWeek;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistory.Commands.DeleteProjectWeek;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Queries.GetProjectWorkBreakdownProgressReport;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Commands.CreateBatchProjectWorkBreakdown;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetProjectWorkBreakdownWithPagination;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Queries.GetProjectWorkBreakdownReport;
using Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetProjectWeekWithPagination;
using Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetProjectWeekCounts;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReport.GetCPIReportWeekly;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReportMonthly;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetCPIReport.GetSPIReportWeekly;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSPIReportMonthly;
using Rasta.ProjectManagment.Application.Project.Queries.GetProjectWithPagination;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistoryWork.Commands.UpdateACProjectWorkBreakdownHistoryWork;
using Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSummaryLevel3;
using Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetCurrentWeek;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get;
using Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Queries.Get;
using Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Queries.Get;
using Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Queries.Get;
using Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Queries.Get;
using Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Queries.Get;
using Rasta.ProjectManagment.Domain.Entities;
using Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Queries.Get;
using Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectHRDailyReport.Queries;
using Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Create;
using Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Update;
using Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Delete;
using Rasta.ProjectManagment.Application.ProjectUsers.AssignCommand;
using Rasta.ProjectManagment.Application.ProjectUsers.AssignQuery;
#endregion

namespace Rasta.ProjectManagment.Api.Controllers
{
    public class ProjectController : BaseApiController
    {
        #region Project

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectBriefVM>>> Get([FromQuery] GetProjectWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<long>>> Create([FromBody] CreateProjectCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<long>>> Update(UpdateProjectCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result<bool>>> Delete(long id)
        {
            return await Mediator.Send(new DeleteProjectCommand(id));
        }
        #endregion

        #region ProjectWorkBreakdown

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> CreateBatchProjectWorkBreakdown([FromForm] CreateBatchProjectWorkBreakdownCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectWorkBreakdownVM>>> GetProjectWorkBreakdown([FromQuery] GetProjectWorkBreakdownQuery query)
        {
            return await Mediator.Send(query);
        }
        #endregion

        #region ProjectWeeks

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectWeekVM>>> GetProjectWeeks([FromQuery] GetProjectWeekQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<ActionResult<ProjectWeekCountVm>> GetProjectWeekCounts([FromQuery] GetProjectWeekCountsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Result<long>>> CreateProjectWeek([FromBody] CreateProjectWeekCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CurrentWeekVm>> GetCurrentWeek([FromQuery] CurrentWeekQuery query)
        {
            return await Mediator.Send(query);
        }

        //[HttpPut()]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> UpdateProjectWorkBreakdownHistory(UpdateProjectBreakdownHistoryCommand command)
        //{
        //    if (command is null || command.Id<=0)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Result<bool>>> DeleteProjectWeek(int id) => await Mediator.Send(new DeleteProjectWeekCommand(id));


        [HttpGet]
        public async Task<ActionResult<List<ProjectWorkBreakdownProgressReportVM>>> GetProjectWorkBreakdownProgressReport([FromQuery] GetProjectWorkBreakdownProgressReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectWorkBreakdownReportVM>>> GetProjectWorkBreakdownReport([FromQuery] GetProjectWorkBreakdownReportQuery query)
        {
            return await Mediator.Send(query);
        }

        #endregion

        #region CPI Report

        [HttpGet]
        public async Task<Result<List<CPIReportWeeklyVM>>> GetCPIWeekly([FromQuery] GetCPIReportWeeklyQuery command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<Result<List<CPIReportMonthlyVM>>> GetCPIMonthly([FromQuery] GetCPIReportMonthlyQuery command)
        {
            return await Mediator.Send(command);
        }
        #endregion

        #region SPI Report

        [HttpGet]
        public async Task<Result<List<SPIReportWeeklyVM>>> GetSPIWeekly([FromQuery] GetSPIReportWeeklyQuery command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<Result<List<SPIReportMonthlyVM>>> GetSPIMonthly([FromQuery] GetSPIReportMonthlyQuery command)
        {
            return await Mediator.Send(command);
        }
        #endregion

        #region Summary of Level3 Report

        [HttpGet]
        public async Task<Result<List<SP_GetSummaryLevel3VM>>> GetSummaryLevel3([FromQuery] GetSummaryLevel3Query query)
        {
            return await Mediator.Send(query);
        }

        #endregion

        #region ProjectExecutionDailyReport

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectExecutionDailyReportBriefVM>>> GetProjectExecutionDailyReport([FromQuery] GetProjectExecutionDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<long>>> CreateProjectExecutionDailyReport([FromBody] CreateProjectExecutionDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<long>>> UpdateProjectExecutionDailyReport(UpdateProjectExecutionDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProjectExecutionDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectExecutionDailyReportCommand(id));
        }
        #endregion

        #region ProjectAccidentDailyReport

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectAccidentDailyReportBriefVM>>> GetProjectAccidentDailyReport([FromQuery] GetProjectAccidentDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<long>>> CreateProjectAccidentDailyReport([FromBody] CreateProjectAccidentDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<long>>> UpdateProjectAccidentDailyReport(UpdateProjectAccidentDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProjectAccidentDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectAccidentDailyReportCommand(id));
        }
        #endregion

        #region ProjectGuestDailyReport

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectGuestDailyReportBriefVM>>> GetProjectGuestDailyReport([FromQuery] GetProjectGuestDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<long>>> CreateProjectGuestDailyReport([FromBody] CreateProjectGuestDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<long>>> UpdateProjectGuestDailyReport(UpdateProjectGuestDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProjectGuestDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectGuestDailyReportCommand(id));
        }
        #endregion

        #region ProjectProblemDailyReport

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectProblemDailyReportBriefVM>>> GetProjectProblemDailyReport([FromQuery] GetProjectProblemDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<long>>> CreateProjectProblemDailyReport([FromBody] CreateProjectProblemDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<long>>> UpdateProjectProblemDailyReport(UpdateProjectProblemDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProjectProblemDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectProblemDailyReportCommand(id));
        }
        #endregion

        #region Project Repairs Daily Report
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectRepairDailyReport>>> GetProjectRepairDailyReport([FromQuery] GetProjectRepairDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> CreateProjectRepairDailyReport([FromBody] CreateProjectRepairsDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<bool>>> UpdateProjectRepairDailyReport(UpdateProjectRepairDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>>
            DeleteProjectRepairDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectRepairDailyReportCommand() { Id = id });
        }
        #endregion

        #region ProjectMachineryDailyReport

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectMachineryDailyReportBriefVM>>> GetProjectMachineryDailyReport([FromQuery] GetProjectMachineryDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<long>>> CreateProjectMachineryDailyReport([FromBody] CreateProjectMachineryDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<long>>> UpdateProjectMachineryDailyReport(UpdateProjectMachineryDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProjectMachineryDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectMachineryDailyReportCommand(id));
        }
        #endregion

        #region Project Materials Daily Report
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectMaterialsDailyReport>>> GetProjectMaterialDailyReport([FromQuery] GetMaterialDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> CreateProjectMaterialDailyReport([FromBody] CreateMaterialDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<bool>>> UpdateProjectMaterialDailyReport(UpdateMaterialDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>>
            DeleteProjectMaterialDailyReport(long id)
        {
            return await Mediator.Send(new DeleteMaterialDailyReportCommand() { Id = id });
        }
        #endregion

        #region Project HR Daily Report
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProjectManpowerDailyReport>>> GetProjectHRDailyReport([FromQuery] GetProjectHRDailyReportQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> CreateProjectHRDailyReport([FromBody] CreateProjectHRDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<bool>>> UpdateProjectHRDailyReport(UpdateProjectHRDailyReportCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProjectHRDailyReport(long id)
        {
            return await Mediator.Send(new DeleteProjectHRDailyReportCommand() { Id = id });
        }
        #endregion


        #region ProjectWBSHistory
        [HttpPost]
        public async Task<ActionResult<Result<Domain.Entities.ProjectWorkBreakdownHistoryWork>>> UpdateAcWBSHistory(UpdateACProjectWorkBreakdownHistoryCommand command)
        {
            return await Mediator.Send(command);
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult<Result<bool>>> AssignUserProjects(ProjectUsersAssignCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<View_ProjectUsers>>>> GetAssignedProjectUsers([FromQuery] ProjectUsersAssignQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<View_ProjectUsers>>>> GetProjectUsersByUser([FromQuery] ProjectUsersByUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpDelete]
        public async Task<ActionResult<Result<bool>>> DeleteProjectUser([FromQuery] int id)
        {
            return await Mediator.Send(new ProjectUsersDeleteCommand() { Id = id });
        }
    }
}
