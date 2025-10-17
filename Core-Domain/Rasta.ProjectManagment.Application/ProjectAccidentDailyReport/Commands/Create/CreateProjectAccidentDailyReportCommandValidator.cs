using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectAccidentDailyReport.Commands.Create
{
    public class CreateProjectAccidentDailyReportCommandValidator : AbstractValidator<CreateProjectAccidentDailyReportCommand>
    {
        public CreateProjectAccidentDailyReportCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
        }
    }
}
