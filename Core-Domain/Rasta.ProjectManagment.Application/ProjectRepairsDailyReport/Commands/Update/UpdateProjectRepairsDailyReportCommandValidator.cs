using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectRepairsDailyReport.Commands.Update
{
    public class UpdateProjectRepairsDailyReportCommandValidator: AbstractValidator<UpdateProjectRepairDailyReportCommand>
    {
        public UpdateProjectRepairsDailyReportCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("شناسه الزامی می باشد");
            RuleFor(v => v.Title).NotEmpty().WithMessage("شرح ماشین/تجهیزات");
            RuleFor(v => v.RepairPlace).NotEmpty().WithMessage("محل انجام تعمیرات");
        }
    }
}
