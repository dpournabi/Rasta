using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Commands.UpdateProjectWorkBreakdown;
public class UpdateProjectWorkBreakdownCommandValidator : AbstractValidator<UpdateProjectWorkBreakdownCommand>
{
    public UpdateProjectWorkBreakdownCommandValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().WithMessage("شناسه پروژه الزامی می باشد");
        RuleFor(v => v.WorkBreakdownStructureId).NotEmpty().WithMessage("شناسه ساختار شکست کار الزامی  می باشد");
        RuleFor(v => v.IsCritical).NotEmpty().WithMessage("حیاتی الزامی می باشد");
        RuleFor(v => v.EstimatedTime).NotEmpty().WithMessage("زمان تخمین زده شده الزامی می باشد");
        RuleFor(v => v.StartDate).NotEmpty().WithMessage("تاریخ شروع الزامی می باشد");
        RuleFor(v => v.EndDate).NotEmpty().WithMessage("تاریخ انتها الزامی می باشد");
        RuleFor(v => v.LastStartDate).NotEmpty().WithMessage("آخرین تاریخ شروع الزامی می باشد");
        RuleFor(v => v.LastEndDate).NotEmpty().WithMessage("آخرین تاریخ انتها الزامی می باشد");
        RuleFor(v => v.Title).NotEmpty().WithMessage("نام فعالیت الزامی می باشد");
    }
}
