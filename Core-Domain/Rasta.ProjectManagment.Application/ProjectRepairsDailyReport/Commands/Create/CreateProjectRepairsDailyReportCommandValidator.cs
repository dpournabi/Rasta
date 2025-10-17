using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Create
{
    public class CreateProjectRepairsDailyReportCommandValidator: AbstractValidator<CreateProjectRepairsDailyReportCommand>
    {
        public CreateProjectRepairsDailyReportCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty().WithMessage("نام پروژه الزامی می باشد");
            RuleFor(v => v.Title).NotEmpty().WithMessage("شرح ماشین/تجهیزات");
            RuleFor(v => v.RepairPlace).NotEmpty().WithMessage("محل انجام تعمیرات");
        }
    }
}
