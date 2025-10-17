using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectTypeRatio.Commands.UpdateProjectTypeRatio;
public class UpdateProjectTypeRatioCommandValidator : AbstractValidator<UpdateProjectTypeRatioCommand>
{
    public UpdateProjectTypeRatioCommandValidator()
    {
        RuleFor(v => v.ProjectTypeId).NotEmpty().WithMessage("نوع پروژه خصوصیت الزامی می باشد");
        RuleFor(v => v.WFBPercentage).NotEmpty().WithMessage("درصد wfbالزامی می باشد");
        RuleFor(v => v.WFTPercentage).NotEmpty().WithMessage("درصد wftp الزامی می باشد");
        RuleFor(v => v.EffectiveDate).NotEmpty().WithMessage("تاریخ الزامی  می باشد");
    }
}
