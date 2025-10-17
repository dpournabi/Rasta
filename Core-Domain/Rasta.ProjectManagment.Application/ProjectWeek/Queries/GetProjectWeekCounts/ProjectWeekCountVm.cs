namespace Rasta.ProjectManagment.Application.ProjectWeek.Queries.GetProjectWeekCounts
{
    public record ProjectWeekCountVm
    {
        public required IEnumerable<WeekVm> Weeks { get; init; }
        public required IEnumerable<MonthVm> Months { get; init; }
    }

    public record WeekVm
    {
        public int Number { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get;  set;}
    }

    public record MonthVm
    {
        public required int Number { get; set; }
        public required string Month { get; set; }
    }
}
