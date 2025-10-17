using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rasta.ProjectManagment.Application.Common.CustomAttribute;
using Rasta.ProjectManagment.Application.Common.FileReaders;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Application.Common.Models;
using Rasta.ProjectManagment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rasta.ProjectManagment.Application.ProjectWorkBreakdown.Commands.CreateBatchProjectWorkBreakdown
{
    public class CreateBatchProjectWorkBreakdownCommand : IRequest<Result<bool>>
    {
        [DataType(DataType.Upload)]
        [MaxFileSize(40 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".xlsx", ".mpp" })]
        public required IFormFile File { get; set; }
        public required int ProjectId { get; set; }
        public bool IsMpp { get; set; }
    }

    public class CreateBatchProjectWorkBreakdownCommandHandler : IRequestHandler<CreateBatchProjectWorkBreakdownCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileReaderFactory _fileReaderFactory;
        private readonly ILogger<CreateBatchProjectWorkBreakdownCommandHandler> _logger;
        private readonly IResourceManager _resourceManager;
        private const string _Operation = "بارگذاری اطلاعات اولیه پروژه";
        public CreateBatchProjectWorkBreakdownCommandHandler(IApplicationDbContext context,
                                                             ILogger<CreateBatchProjectWorkBreakdownCommandHandler> logger,
                                                             IResourceManager resourceManager,
                                                             IFileReaderFactory fileReaderFactory)
        {
            _context = context;
            _logger = logger;
            _resourceManager = resourceManager;
            _fileReaderFactory = fileReaderFactory;
        }
        public async Task<Result<bool>> Handle(CreateBatchProjectWorkBreakdownCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy executionStrategy = _context.GetDatabase().CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _context.GetDatabase().BeginTransactionAsync(isolationLevel: System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        await ImportDataFromExcell(request, cancellationToken);
                        List<Domain.Entities.ProjectWorkBreakdown> projectItems = await UpdateWeightFactors(request.ProjectId, cancellationToken);
                        await GenerateAllWeeksByProjectDuration(projectItems, cancellationToken);
                        await GenerateWorkItemHistory(projectItems, cancellationToken);
                        await UpdatePlanCompleteProgressPercentageAsnyc(request.ProjectId, cancellationToken);
                        await UpdateActualCompleteProgressPercentageAsnyc(request.ProjectId, cancellationToken);
                        await transaction.CommitAsync();

                        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Success.ToString()), _Operation);
                        return await Task.FromResult(Result<bool>.Success(message, true));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        message = string.Format(_resourceManager.GetResxNameByValue(MessageTypes.Failer.ToString()), _Operation);
                        return await Task.FromResult(Result<bool>.Failure(message, new string[] { ex.InnerException != null ? ex.InnerException.Message : ex.Message }, false));
                    }
                }
            });
        }

        #region Private Functions...

        private async Task ImportDataFromExcell(CreateBatchProjectWorkBreakdownCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Project? project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
                throw new ArgumentException("پروژه ای با شناسه ارسالی یافت نشد");

            await BulkUpdateToDisableOldData(request, cancellationToken);

            FileReaderTypes fileTypeReader = request.IsMpp ? FileReaderTypes.MppReader : FileReaderTypes.ExcelReader;
            Common.FileReaders.Providers.FileReaderAbstraction fileReader = await _fileReaderFactory.Get(fileTypeReader);
            List<Domain.Entities.ProjectWorkBreakdown> projectWorkBreakdowns = await fileReader.ReadData(request.ProjectId, request.File);
            await _context.ProjectWorkBreakdowns.AddRangeAsync(projectWorkBreakdowns);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogDebug("Finish Import data from excel...");
        }
        private async Task BulkUpdateToDisableOldData(CreateBatchProjectWorkBreakdownCommand request, CancellationToken cancellationToken)
        {
            int data = await _context.ProjectWorkBreakdowns
                                     .Where(x => x.ProjectId == request.ProjectId)
                                     .ExecuteUpdateAsync(setter => setter.SetProperty(pb => pb.IsDelete, true), cancellationToken)
                                     .ConfigureAwait(false);

            int projectWeeks = await _context.ProjectWeekes
                                             .Where(x => x.ProjectId == request.ProjectId)
                                             .ExecuteUpdateAsync(setter => setter.SetProperty(pb => pb.IsDelete, true), cancellationToken)
                                             .ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        private async Task<List<Domain.Entities.ProjectWorkBreakdown>> UpdateWeightFactors(int projectId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Start calculating WeightFactor...");
            List<Domain.Entities.ProjectWorkBreakdown> projectItems = await _context.ProjectWorkBreakdowns
                                             //.Include(x => x.WorkBreakdownStructure)
                                             .Include(x => x.Project)
                                             .ThenInclude(x => x.ProjectType)
                                             .ThenInclude(x => x.ProjectTypeRatios)
                                             .Where(x => x.ProjectId == projectId && x.IsLastNode && !x.IsDelete)
                                             .ToListAsync(cancellationToken)
                                             .ConfigureAwait(false);

            int totalDuration = projectItems.Sum(x => x.Duration);
            foreach (Domain.Entities.ProjectWorkBreakdown? projectItem in projectItems)
            {
                projectItem.WeightFactorTime = projectItem.Duration / (double)totalDuration;
                projectItem.WeightFactor = projectItem.WeightFactorTime;

                if (projectItem.WeightFactorBudject is not null)
                {
                    Domain.Entities.ProjectTypeRatio? projectTypeRatio = projectItem.Project?.ProjectType?.ProjectTypeRatios.MaxBy(x => x.EffectiveDate);
                    projectItem.WeightFactor = (projectTypeRatio?.WFTPercentage * projectItem.WeightFactorTime) + (projectTypeRatio?.WFBPercentage * projectItem.WeightFactorBudject);
                }
            }

            //Process history
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _logger.LogDebug("Finish calculating WeightFactor...");

            return await Task.FromResult(projectItems).ConfigureAwait(false);
        }
        private async Task<bool> GenerateAllWeeksByProjectDuration(List<Domain.Entities.ProjectWorkBreakdown> projectItems, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Start generating weeks...");
            List<Domain.Entities.ProjectWorkBreakdown> workItems = projectItems
                                .OrderBy(x => x.StartDate)
                                .ThenBy(x => x.EndDate)
                                .ToList();

            DateTime minDateItem = workItems.Min(x => x.StartDate);
            DateTime maxDateItem = workItems.Max(x => x.EndDate);
            Domain.Entities.Project project = workItems.First().Project;

            if (maxDateItem == minDateItem)
            {
                throw new Exception("تاریخ شروع و پایان پروژه نمی تواند یکسان باشد");
            }

            double _totalDays = (maxDateItem - minDateItem).TotalDays;
            int weekCounts = Convert.ToInt32(Math.Ceiling(_totalDays / 7));
            int _monthNumber = 1;

            for (int i = 0; i < weekCounts; i++)
            {
                DateTime _startWeekTime = minDateItem.AddDays(i * 7);
                DateTime _endWeekTime = _startWeekTime.AddDays(7);
                if (_endWeekTime > maxDateItem)
                    _endWeekTime = maxDateItem;


                Domain.Entities.ProjectWeek entity = new()
                {
                    ProjectId = project.Id,
                    StartDate = _startWeekTime,
                    EndDate = _endWeekTime,
                    WeekCount = i + 1,
                    IsPlan = true,
                    IsDelete = false
                };

                _monthNumber = entity.WeekCount % 4 == 0 ? (entity.WeekCount / 4) + 1 : _monthNumber;
                entity.MonthNumber = _monthNumber;

                await _context.ProjectWeekes.AddAsync(entity).ConfigureAwait(false);
            }
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogDebug("Complete processing project plans...");
            return await Task.FromResult(true);
        }
        private async Task GenerateWorkItemHistory(List<Domain.Entities.ProjectWorkBreakdown> projectItems, CancellationToken cancellationToken)
        {
            Domain.Entities.Project project = projectItems.First().Project;
            IQueryable<Domain.Entities.ProjectWeek> allweeks = _context.ProjectWeekes
                                   .Where(x => x.ProjectId == project.Id &&
                                               !x.IsDelete)
                                   .AsQueryable();

            List<Domain.Entities.ProjectWorkBreakdownHistoryWork> projectWorkBreakdownHistoryWorkItems = new();
            //var allprojectWorkbreakdownStructures = await _context.WorkBreakdownStructures.ToListAsync(cancellationToken).ConfigureAwait(false);

            foreach (Domain.Entities.ProjectWorkBreakdown workItem in projectItems)
            {
                List<Domain.Entities.ProjectWeek> weeks = allweeks.Where(x => ((x.StartDate >= workItem.StartDate && x.StartDate <= workItem.EndDate) ||
                                                            x.EndDate >= workItem.StartDate && x.EndDate <= workItem.EndDate) ||
                                                            ((x.StartDate <= workItem.StartDate && x.EndDate >= workItem.StartDate) ||
                                                   x.StartDate <= workItem.EndDate && x.EndDate >= workItem.EndDate)
                                           ).Distinct().ToList();

                int counter = 1;
                foreach (Domain.Entities.ProjectWeek? week in weeks)
                {
                    //Plan
                    double currentWorkDays = 7;

                    if (counter == 1)
                    {
                        currentWorkDays = (week.EndDate.Date - workItem.StartDate.Date).TotalDays;
                    }
                    else if (weeks.Count == counter)
                    {
                        currentWorkDays = (workItem.EndDate.Date - week.StartDate.Date).TotalDays;
                    }


                    if (currentWorkDays > 7)
                        currentWorkDays = 7;

                    //double totalWorkDays = workItem.Duration; //(workItem.EndDate.Date - workItem.StartDate.Date).TotalDays;
                    double ratio = currentWorkDays * 100 / workItem.Duration;

                    double? sumOfPlanPercentageBeforeWeeks = projectWorkBreakdownHistoryWorkItems
                                                           .Where(x => x.ProjectWorkBreakdownId == workItem.Id)
                                                           .Sum(x => x.PlanPercentage);
                    //Real
                    Random rnd = new();
                    int digit = rnd.Next(7, 10);
                    decimal fakeData = digit * 0.1m;

                    double? sumOfRealPercentageBeforeRealWeeks = projectWorkBreakdownHistoryWorkItems
                                                           .Where(x => x.ProjectWorkBreakdownId == workItem.Id)
                                                           .Sum(x => x.RealPercentage);

                    if (sumOfPlanPercentageBeforeWeeks == 100)
                        continue;

                    if (ratio > 100)
                        ratio = 100;

                    double? cumulativeRatio = sumOfPlanPercentageBeforeWeeks + ratio;
                    double? cumulativeRealRatio = sumOfRealPercentageBeforeRealWeeks + (((double)fakeData) * ratio);

                    if (!projectWorkBreakdownHistoryWorkItems.Any(x => x.ProjectWeekId == week.Id && x.WorkBreakdownStructureCode == workItem.WorkBreakdownStructureCode && x.PlanPercentage == 100))
                    {
                        if (ratio is double.NaN)
                            continue;

                        Domain.Entities.ProjectWorkBreakdownHistoryWork newItem = new();
                        newItem.ProjectWeekId = week.Id;
                        newItem.ProjectWorkBreakdownId = workItem.Id;
                        newItem.WorkBreakdownStructureCode = workItem.WorkBreakdownStructureCode;
                        newItem.PlanPercentage = ratio;
                        newItem.PlanCumulativePercentage = cumulativeRatio;
                        newItem.PlanCompleteProgressPercentage = ratio * workItem.WeightFactor;
                        newItem.PlanWeightProgressPercentage = (cumulativeRatio * workItem.WeightFactor);
                        //Insert fake data for demo
                        newItem.RealPercentage = ratio * ((double)fakeData);
                        newItem.RealCumulativePercentage = cumulativeRealRatio;
                        newItem.ActualCompleteProgressPercentage = ratio * ((double)fakeData) * workItem.WeightFactor;
                        newItem.ActualWeightProgressPercentage = cumulativeRealRatio * workItem.WeightFactor;
                        newItem.IsDone = false;
                        newItem.EV = workItem.Budjet != null ? workItem.Budjet.Value * Convert.ToDecimal(ratio * ((double)fakeData)) : 0;
                        newItem.PV = workItem.Budjet != null ? workItem.Budjet.Value * Convert.ToDecimal(ratio) : 0;
                        newItem.ACB = workItem.Budjet.Value * Convert.ToDecimal(ratio) * (decimal)0.06; //fake data
                        newItem.SV = (workItem.Budjet != null ? workItem.Budjet.Value * Convert.ToDecimal(ratio * ((double)fakeData)) : 0) - (workItem.Budjet != null ? workItem.Budjet.Value * Convert.ToDecimal(ratio) : 0);//EV-PV
                        newItem.CV = (workItem.Budjet != null ? workItem.Budjet.Value * Convert.ToDecimal(ratio * ((double)fakeData)) : 0) - workItem.Budjet.Value * Convert.ToDecimal(ratio) * (decimal)0.06;//EV-ACB
                        projectWorkBreakdownHistoryWorkItems.Add(newItem);
                    }
                    //var workBreakdownStructure = allprojectWorkbreakdownStructures.FirstOrDefault(x => x.Code == workItem.WorkBreakdownStructureCode);
                    //if (workBreakdownStructure?.Level == 5)//Update positive and negative floor count info in project table
                    //    await UpdateProjectFloorInfoesAsnyc(workItem.ProjectId, workItem.Floor.Value, cancellationToken);

                    counter++;
                }

                List<Domain.Entities.ProjectWorkBreakdownHistoryWork> currentWorkItemHistories = projectWorkBreakdownHistoryWorkItems.Where(x => x.ProjectWorkBreakdownId == workItem.Id).ToList();
                decimal? sumOfEV = currentWorkItemHistories.Sum(x => x.EV);
                decimal? sumOfPv = currentWorkItemHistories.Sum(x => x.PV);
                decimal? sumOfACB = currentWorkItemHistories.Sum(x => x.ACB);
                workItem.SPI = (sumOfEV > 0 && sumOfPv > 0) ? sumOfEV / sumOfPv : 0;
                workItem.CPI = (sumOfEV > 0 && sumOfACB > 0) ? sumOfEV / sumOfACB : 0;

            }
            await _context.ProjectWorkBreakdownHistoryWorks.AddRangeAsync(projectWorkBreakdownHistoryWorkItems).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }
        private async Task UpdatePlanCompleteProgressPercentageAsnyc(int projectId, CancellationToken cancellationToken)
        {
            Domain.Entities.Project? project = await _context.Projects
                                        .Include(x => x.ProjectWeeks)
                                        .ThenInclude(projectWeek => projectWeek.ProjectWorkBreakdownHistoryWorks)
                                        .FirstAsync(x => x.Id == projectId);
            if (project is null)
                return;
            IOrderedEnumerable<Domain.Entities.ProjectWorkBreakdownHistoryWork> weeks = project.ProjectWeeks
                               .SelectMany(x => x.ProjectWorkBreakdownHistoryWorks)
                               .OrderBy(x => x.ProjectWeekId)
                               .ThenBy(x => x.PlanPercentage);
            double SumOfPlanCompleteProgressPercentage = 0;
            foreach (Domain.Entities.ProjectWorkBreakdownHistoryWork? item in weeks)
            {
                SumOfPlanCompleteProgressPercentage += item.PlanCompleteProgressPercentage ?? 0;
                item.CumulativePlanCompleteProgressPercentage = SumOfPlanCompleteProgressPercentage;
            }
            await _context.SaveChangesAsync(cancellationToken);

        }
        private async Task UpdateActualCompleteProgressPercentageAsnyc(int projectId, CancellationToken cancellationToken)
        {
            Domain.Entities.Project? project = await _context.Projects
                                        .Include(x => x.ProjectWeeks)
                                        .ThenInclude(projectWeek => projectWeek.ProjectWorkBreakdownHistoryWorks)
                                        .FirstAsync(x => x.Id == projectId);
            if (project is null)
                return;
            IOrderedEnumerable<Domain.Entities.ProjectWorkBreakdownHistoryWork> weeks = project.ProjectWeeks
                               .SelectMany(x => x.ProjectWorkBreakdownHistoryWorks)
                               .OrderBy(x => x.ProjectWeekId)
                               .ThenBy(x => x.PlanPercentage);
            double SumOfActualCompleteProgressPercentage = 0;
            foreach (Domain.Entities.ProjectWorkBreakdownHistoryWork? item in weeks)
            {
                SumOfActualCompleteProgressPercentage += item.ActualCompleteProgressPercentage ?? 0;
                item.CumulativeActualCompleteProgressPercentage = SumOfActualCompleteProgressPercentage;
            }
            await _context.SaveChangesAsync(cancellationToken);

        }
        private async Task UpdateProjectFloorInfoesAsnyc(int projectId, int floor, CancellationToken cancellationToken)
        {
            Domain.Entities.Project? project = await _context.Projects.FindAsync(projectId);
            if (project is null)
                return;

            switch (floor)
            {
                case < 0:
                    if (project.NegativeFloorCount == null || Math.Abs(project.NegativeFloorCount.Value) < Math.Abs(floor))
                        project.NegativeFloorCount = floor;
                    break;
                case > 0:
                    if (project.PositiveFloorCount == null || project.PositiveFloorCount.Value < floor)
                        project.PositiveFloorCount = floor;
                    break;
                case 0:
                    project.HasGroundFloor = true;
                    break;
            }
        }

        #endregion
    }
}
