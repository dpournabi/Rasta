using FluentValidation;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Queries.Get;

public class GetProjectMachineryDailyReportValidator : AbstractValidator<GetProjectExecutionDailyReportQuery>
{
    public GetProjectMachineryDailyReportValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
    }
}
