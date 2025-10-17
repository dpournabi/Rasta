//using OfficeOpenXml;
//using System.Globalization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Rasta.ProjectManagment.Application.Common.Interfaces;

//namespace Rasta.ProjectManagment.Application.Common.FileReaders.Providers;

//public class ExcelReader : FileReaderAbstraction
//{
//    public ExcelReader(IApplicationDbContext context, IDateTimeService dateTimeService) : 
//        base(context, dateTimeService)
//    {
//    }

//    public override async Task<List<Domain.Entities.ProjectWorkBreakdown>> ReadData(int projectId, IFormFile file)
//    {
//        var data = await ConvertIFormFileToByteArray(file);
//        //var wbsData = await _context.WorkBreakdownStructures.ToListAsync();
//        string fullPath = await ConvertByteArrayToFile(data, "xlsx");
//        using var package = new ExcelPackage(fullPath);
//        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        var currentSheet = package.Workbook.Worksheets;
//        var workSheet = currentSheet.First();
//        var noOfCol = workSheet.Dimension.End.Column;
//        var noOfRow = workSheet.Dimension.End.Row;

//        var excelData = new List<Domain.Entities.ProjectWorkBreakdown>();
//        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
//        {
//            string? code = workSheet.Cells[rowIterator, 1].Value.ToString();
//            if (string.IsNullOrWhiteSpace(code))
//                continue;

//            string[] codeParts = code.Split('.');
//            if (codeParts[0].Trim() == "1")
//                continue;

//            string? floor = workSheet.Cells[rowIterator, 3].Value?.ToString();
//            //string? _floor = floor switch
//            //{
//            //    "بام" => "100",
//            //    "همه" => "101",
//            //    _ => floor
//            //};
//            //var _WorkBreakdownStructureId = await FindWorkBreakdownStructureId(code);
//            //if (_WorkBreakdownStructureId is null)
//            //    throw new Exception($"کد WBS {workSheet.Cells[rowIterator, 1].Value} معتبر نمی باشد!");

//            //var childCounts = wbsData.Count(x => x.ParentId == _WorkBreakdownStructureId);
//            var projectWorkBreakdown = new Domain.Entities.ProjectWorkBreakdown
//            {
//                ProjectId = projectId,
//                //WorkBreakdownStructureId = _WorkBreakdownStructureId,
//                WorkBreakdownStructureCode = workSheet.Cells[rowIterator, 1].Value?.ToString().Trim(),
//                //Code = Convert.ToInt64(workSheet.Cells[rowIterator, 2].Value),
//                Title = workSheet.Cells[rowIterator, 2].Value?.ToString().Trim(),
//                Floor = floor, //_floor != null ? Convert.ToInt32(_floor) : null,
//                StartDate = await TryParseStringToDateAsync(workSheet.Cells[rowIterator, 5].Value?.ToString().Trim()),
//                EndDate = await TryParseStringToDateAsync(workSheet.Cells[rowIterator, 6].Value?.ToString().Trim()),
//                IsCritical = workSheet.Cells[rowIterator, 7].Value != null && workSheet.Cells[rowIterator, 7].Value.ToString().ToLower() == "yes" ? true : false,
//                Predecessors = workSheet.Cells[rowIterator, 8].Value != null ? workSheet.Cells[rowIterator, 8].Value.ToString().Trim() : "",
//                Successors = workSheet.Cells[rowIterator, 9].Value != null ? workSheet.Cells[rowIterator, 9].Value.ToString().Trim() : "",
//                LastStartDate = await TryParseStringToDateAsync(workSheet.Cells[rowIterator, 10].Value?.ToString().Trim()),
//                LastEndDate = await TryParseStringToDateAsync(workSheet.Cells[rowIterator, 11].Value?.ToString().Trim()),
//                Budjet = Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value.ToString().Trim()),
//               // IsLastNode = childCounts == 0,
//            };

//            projectWorkBreakdown.Duration = Convert.ToInt32((projectWorkBreakdown.EndDate - projectWorkBreakdown.StartDate).TotalDays);
//            excelData.Add(projectWorkBreakdown);
//        }
//        return excelData;
//    }
//    private async Task<DateTime> TryParseStringToDateAsync(string strDatetime)
//    {
//        //Try convert gregorian string datetime to gregorian time 
//        DateTime convertedDate = DateTime.MinValue;
//        var culture = CultureInfo.CreateSpecificCulture("en-US");
//        DateTime.TryParse(strDatetime, culture, out convertedDate);

//        //Try convert persian strin datetime to gregorian time 
//        if (convertedDate == DateTime.MinValue)
//        {
//            culture = CultureInfo.CreateSpecificCulture("fa-IR");
//            convertedDate = _dateTimeService.ConvertPersianToGregurianDate(strDatetime);
//        }

//        return await Task.FromResult(convertedDate);
//    }
//}
