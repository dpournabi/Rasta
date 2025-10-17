using FluentValidation;

namespace Rasta.ProjectManagment.Application.MeasureUnit.Commands.CreateMeasureUnit;
public class CreateMeasureUnitCommandValidator : AbstractValidator<CreateMeasureUnitCommand>
{
    public CreateMeasureUnitCommandValidator()
    {
        RuleFor(v => v.TitleEn).NotEmpty().WithMessage("عنوان انگلیسی الزامی می باشد").MaximumLength(10).WithMessage("تعداد عنوان انگلیسی بیشتر از 10 کاراکتر نمی باشد.");
        RuleFor(v => v.Title).NotEmpty().WithMessage("عنوان الزامی می باشد").MaximumLength(50).WithMessage("تعداد عنوان بیشتر از 50 کاراکتر نمی باشد");
        //RuleFor(v => v.IsDefault).NotEmpty().WithMessage("مقدار پیش فرض الزامی می باشد");
    }
}
