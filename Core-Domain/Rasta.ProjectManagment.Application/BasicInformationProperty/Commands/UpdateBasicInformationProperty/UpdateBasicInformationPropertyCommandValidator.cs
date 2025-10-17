using FluentValidation;
using Rasta.ProjectManagment.Application.BasicInformation.Commands.UpdateBasicInformation;

namespace Rasta.ProjectManagment.Application.BasicInformationProperty.Commands.UpdateBasicInformationProperty;

public class UpdateBasicInformationPropertyCommandValidator : AbstractValidator<UpdateBasicInformationPropertyCommand>
{
    public UpdateBasicInformationPropertyCommandValidator()
    {
        RuleFor(v => v.BasicInformationId).NotEmpty().WithMessage("شناسه اطلاعات پایه خصوصیت الزامی می باشد");
        RuleFor(v => v.PropertyId).NotEmpty().WithMessage("شناسه خصوصیت الزامی می باشد");
        RuleFor(v => v.Value).NotEmpty().WithMessage("مقدار الزامی می باشد").MaximumLength(50).WithMessage("مقدار نمی تواند بیشتر از 50 کاراکتر باشد.");
    }
}

