using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectExecutionDailyReport.Commands.Create
{
    public class CreateProjectExecutionDailyReportCommandValidator : AbstractValidator<CreateProjectExecutionDailyReportCommand>
    {
        public CreateProjectExecutionDailyReportCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
        }
    }
}
