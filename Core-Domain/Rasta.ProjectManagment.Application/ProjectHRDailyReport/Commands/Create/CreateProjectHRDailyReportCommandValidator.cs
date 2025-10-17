using FluentValidation;
using Rasta.ProjectManagment.Application.ProjectMaterialDailyReport.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasta.ProjectManagment.Application.ProjectHRDailyReport.Commands.Create
{
    internal class CreateProjectHRDailyReportCommandValidator:
        AbstractValidator<CreateProjectHRDailyReportCommand>
    {
        public CreateProjectHRDailyReportCommandValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("پروژه ای وارد نشده است!");
        }
    }
}
