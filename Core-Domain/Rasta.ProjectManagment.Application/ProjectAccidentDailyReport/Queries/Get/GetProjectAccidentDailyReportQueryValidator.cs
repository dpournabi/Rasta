using FluentValidation;
using Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Queries.Get;

public class GetProjectAccidentDailyReportQueryValidator : AbstractValidator<GetProjectExecutionDailyReportQuery>
{
    public GetProjectAccidentDailyReportQueryValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
    }
}
