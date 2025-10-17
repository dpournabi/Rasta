using FluentValidation;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.CreateBasicInformationProperty;
public class CreateBasicInformationPropertyCommandValidator : AbstractValidator<CreateBasicInformationPropertyCommand>
{
    public CreateBasicInformationPropertyCommandValidator()
    {
        RuleFor(v => v.BasicInformationId).NotEmpty().WithMessage("شناسه اطلاعات پایه خصوصیت الزامی می باشد");
        RuleFor(v => v.PropertyId).NotEmpty().WithMessage("شناسه خصوصیت الزامی می باشد");
        RuleFor(v => v.Value).NotEmpty().WithMessage("مقدار الزامی می باشد").MaximumLength(50).WithMessage("مقدار نمی تواند بیشتر از 50 کاراکتر باشد.");
    }

}

