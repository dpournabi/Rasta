using FluentValidation;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Queries.GetSummaryLevel3;
public class GetSummaryLevel3QueryValidator : AbstractValidator<GetSummaryLevel3Query>
{
    public GetSummaryLevel3QueryValidator()
    {
        RuleFor(v => v.ProjectId).NotEmpty().NotNull().WithMessage("شناسه پروژه الزامی می باشد");
        RuleFor(v => v.WeekNumber).NotEmpty().NotNull().WithMessage("ارسال شماره هفته الزامی می باشد");
      }
}
