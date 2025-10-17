using FluentValidation;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Queries.Get;

public class GetProjectGuestDailyReportQueryValidator : AbstractValidator<GetProjectExecutionDailyReportQuery>
{
    public GetProjectGuestDailyReportQueryValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
    }
}
