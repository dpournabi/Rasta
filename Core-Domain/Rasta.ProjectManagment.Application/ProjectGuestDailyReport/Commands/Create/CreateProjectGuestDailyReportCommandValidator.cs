using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectGuestDailyReport.Commands.Create
{
    public class CreateProjectGuestDailyReportCommandValidator : AbstractValidator<CreateProjectGuestDailyReportCommand>
    {
        public CreateProjectGuestDailyReportCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
        }
    }
}
