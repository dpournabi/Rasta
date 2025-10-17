using FluentValidation;

namespace Rasta.ProjectManagment.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(v => v.ProjectName).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
            RuleFor(v => v.InfrastructureArea).NotEmpty().GreaterThan(0).WithMessage("مقدار متراژ پروژه الزامی است");
            RuleFor(v => v.UnitCount).NotEmpty().GreaterThan(0).WithMessage("تعداد واحد الزامی می باشد");
            RuleFor(v => v.FloorCount).NotEmpty().GreaterThan(0).WithMessage("تعداد طبقه الزامی می باشد");
            RuleFor(v => v.EmployerName).NotEmpty().WithMessage("نام کارفرما الزامی می باشد");
            RuleFor(v => v.ProjectTypeId).NotEmpty().GreaterThan(0).WithMessage("نوع پروژه الزامی می باشد");
            RuleFor(v => v.StartContractDate).GreaterThan(DateTime.MinValue).WithMessage("تاریخ شروع پروژه الزامی می باشد");
            RuleFor(v => v.EndContractDate).GreaterThan(DateTime.MinValue).WithMessage("تاریخ پایان پروژه الزامی می باشد");
        }
    }
}
