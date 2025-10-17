using FluentValidation;

namespace Rasta.ProjectManagment.Application.Property.Commands.UpdateProperty;
public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("نام خصوصیت الزامی می باشد").MaximumLength(50).WithMessage("نام خصوصیت نمی تواند بیشتر از 50 کاراکتر باشد.");
        RuleFor(v => v.Type).NotEmpty().WithMessage("نوع خصوصیت الزامی می باشد").MaximumLength(10).WithMessage("نوع خصوصیت نمی تواند بیشتر از 10 کاراکتر باشد.");
    }
}
