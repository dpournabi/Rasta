using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Queries.Get
{
    public class GetProjectExecutionDailyReportQueryValidator : AbstractValidator<GetProjectExecutionDailyReportQuery>
    {
        public GetProjectExecutionDailyReportQueryValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
        }
    }
}
