using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Update
{
    public class UpdateMaterialDailyReportCommandValidator: 
        AbstractValidator<UpdateMaterialDailyReportCommand>
    {
        public UpdateMaterialDailyReportCommandValidator()
        {
            RuleFor(x => x.UnitId).GreaterThan(0).WithMessage("واحد اندازه گیری وارد نشده است!");
            RuleFor(x => x.MaterialsDescription).NotEmpty().WithMessage("شرح مصالح الزامی است!");
            RuleFor(x => x.ImportAmount).NotEmpty().WithMessage("مقدار وارده در روز الزامی است!");
        }
    }
}
