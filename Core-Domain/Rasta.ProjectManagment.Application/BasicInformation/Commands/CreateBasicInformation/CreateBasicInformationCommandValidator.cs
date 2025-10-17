using FluentValidation;

namespace Rasta.ProjectManagment.Application.BasicInformation.Commands.CreateBasicInformation;
public class CreateBasicInformationCommandValidator : AbstractValidator<CreateBasicInformationCommand>
{
    public CreateBasicInformationCommandValidator()
    {
        RuleFor(v => v.Code).NotEmpty().WithMessage("کد الزامی می باشد").MaximumLength(10).WithMessage("تعداد کد بیشتر از 10 کاراکتر نمی باشد.");
        RuleFor(v => v.Name).NotEmpty().WithMessage("نام الزامی می باشد").MaximumLength(50).WithMessage("تعداد نام بیشتر از 50 کاراکتر نمی باشد");
        RuleFor(v => v.Level).NotEmpty().WithMessage("مرحله الزامی می باشد");
    }
}
