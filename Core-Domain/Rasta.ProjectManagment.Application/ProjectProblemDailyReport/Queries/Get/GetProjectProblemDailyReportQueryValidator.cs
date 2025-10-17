using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Queries.Get;

public class GetProjectProblemDailyReportQueryValidator : AbstractValidator<GetProjectProblemDailyReportQuery>
{
    public GetProjectProblemDailyReportQueryValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
    }
}
