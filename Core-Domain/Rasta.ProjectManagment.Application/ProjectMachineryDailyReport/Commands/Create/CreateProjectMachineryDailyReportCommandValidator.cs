using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectMachineryDailyReport.Commands.Create
{
    public class CreateProjectMachineryDailyReportCommandValidator : AbstractValidator<CreateProjectMachineryDailyReportCommand>
    {
        public CreateProjectMachineryDailyReportCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
        }
    }
}
