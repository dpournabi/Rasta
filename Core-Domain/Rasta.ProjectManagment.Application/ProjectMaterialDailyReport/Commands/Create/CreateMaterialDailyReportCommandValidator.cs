using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Create
{
    public class CreateMaterialDailyReportCommandValidator: 
        AbstractValidator<CreateMaterialDailyReportCommand>
    {
        public CreateMaterialDailyReportCommandValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("پروژه ای وارد نشده است!");
            RuleFor(x => x.UnitId).GreaterThan(0).WithMessage("واحد اندازه گیری وارد نشده است!");
            RuleFor(x => x.MaterialsDescription).NotEmpty().WithMessage("شرح مصالح الزامی است!");
            RuleFor(x => x.ImportAmount).NotEmpty().WithMessage("مقدار وارده در روز الزامی است!");
        }
    }
}
