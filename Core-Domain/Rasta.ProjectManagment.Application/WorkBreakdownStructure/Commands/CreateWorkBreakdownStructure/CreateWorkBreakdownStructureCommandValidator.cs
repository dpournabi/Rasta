//using FluentValidation;

//namespace Rasta.ProjectManagment.Application.WorkBreakdownStructure.Commands.CreateWorkBreakdownStructure;
//public class CreateWorkBreakdownStructureCommandValidator : AbstractValidator<CreateWorkBreakdownStructureCommand>
//{
//    public CreateWorkBreakdownStructureCommandValidator()
//    {
//        RuleFor(v => v.Code).NotEmpty().WithMessage("کد الزامی می باشد").MaximumLength(10).WithMessage("تعداد کد بیشتر از 10 کاراکتر نمی باشد.");
//        RuleFor(v => v.BudjetCode).NotEmpty().WithMessage("کد بودجه الزامی  می باشد").MaximumLength(10).WithMessage("تعداد کد بودجه بیشتر از 10 کاراکتر نمی باشد");
//        RuleFor(v => v.Title).NotEmpty().WithMessage("عنوان الزامی می باشد").MaximumLength(50).WithMessage("تعداد عنوان بیشتر از 10 کاراکتر نمی باشد");
//        RuleFor(v => v.TitleEn).NotEmpty().WithMessage("عنوان انگلیسی الزامی می باشد").MaximumLength(10).WithMessage("تعداد عنوان انگلیسی از 10 کاراکتر نمی باشد");
//    }
//}
