using FluentValidation;
using Rasta.ProjectManagment.Application.ProjectWeek.Commands.CreateProjectWeek;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdownHistory.Commands.CreateProjectWorkBreakdownHistory;
public class CreateProjectWeekCommandValidator : AbstractValidator<CreateProjectWeekCommand>
{
    public CreateProjectWeekCommandValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().WithMessage("شناسه پروژه خصوصیت الزامی می باشد");
        RuleFor(v => v.WeekCount).NotEmpty().WithMessage("تاریخ شروع الزامی می باشد");
        RuleFor(v => v.StartDate).NotEmpty().WithMessage("تاریخ شروع الزامی می باشد");
        RuleFor(v => v.EndDate).NotEmpty().WithMessage("تاریخ پایان الزامی می باشد");
    }
}
