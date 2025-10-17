using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectProblemDailyReport.Commands.Create
{
    public class CreateProjectProblemDailyReportCommandValidator : AbstractValidator<CreateProjectProblemDailyReportCommand>
    {
        public CreateProjectProblemDailyReportCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
        }
    }
}
