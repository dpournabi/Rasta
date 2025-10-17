using FluentValidation;

namespace Rasta.ProjectManagment.Application.LookUp.Commands.CreateLookUp;
public class CreateLookUpCommandValidator : AbstractValidator<CreateLookUpCommand>
{
    public CreateLookUpCommandValidator()
    {
        RuleFor(v => v.Title).NotEmpty().WithMessage("عنوان الزامی می باشد").MaximumLength(100).WithMessage("تعداد عنوان بیشتر از 100 کاراکتر نمی باشد.");
        RuleFor(v => v.Code).NotEmpty().WithMessage("کد الزامی  می باشد").MaximumLength(10).WithMessage("تعداد کد بیشتر از 10 کاراکتر نمی باشد");
        RuleFor(v => v.Type).NotEmpty().WithMessage("نوع الزامی  می باشد").MaximumLength(50).WithMessage("تعداد نوع بیشتر از 50 کاراکتر نمی باشد");
    }
}
