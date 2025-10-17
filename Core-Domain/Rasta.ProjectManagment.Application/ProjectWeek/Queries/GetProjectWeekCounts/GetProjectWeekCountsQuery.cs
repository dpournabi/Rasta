using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetProjectWeekCounts
{
    public class GetProjectWeekCountsQuery : IRequest<ProjectWeekCountVm>
    {
        public required int ProjectId { get; set; }
    }
    public class GetProjectWeekCountsCommandHandler : IRequestHandler<GetProjectWeekCountsQuery, ProjectWeekCountVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;

        public GetProjectWeekCountsCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }

        public async Task<ProjectWeekCountVm> Handle(GetProjectWeekCountsQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.ProjectWeek> weeks = await _context.ProjectWeekes
                                      .Where(x => x.ProjectId == request.ProjectId && !x.IsDelete)
                                      .OrderBy(x => x.WeekCount)
                                      .ToListAsync();

            return await Task.FromResult(new ProjectWeekCountVm
            {
                Months = weeks.Select(x => new MonthVm { Number = x.MonthNumber, Month = x.MonthNumber.ToString() }).Distinct().ToList(),
                Weeks = weeks.Select(x => new WeekVm
                {
                    Number = x.WeekCount,
                    StartDate = _dateTimeService.GetPersianDateString(x.StartDate),
                    EndDate = _dateTimeService.GetPersianDateString(x.EndDate.AddDays(-1))
                }).Distinct().ToList()
            });
        }
    }

}
