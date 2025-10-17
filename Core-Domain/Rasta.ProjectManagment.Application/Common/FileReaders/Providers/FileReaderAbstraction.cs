using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using System.Reflection;

namespace Rasta.ProjectManagment.Application.Common.FileReaders.Providers
{
    public abstract class FileReaderAbstraction
    {
        protected readonly IApplicationDbContext _context;
        protected readonly IDateTimeService _dateTimeService;
        public FileReaderAbstraction(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }
        public abstract Task<List<Domain.Entities.ProjectWorkBreakdown>> ReadData(int projectId, IFormFile file);
        protected async Task<byte[]> ConvertIFormFileToByteArray(IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                ms.Position = 0;
                return await Task.FromResult(ms.ToArray());
            }
        }
        protected async Task<string> ConvertByteArrayToFile(byte[] bytes, string fileType)
        {
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Temp";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string filePath = Path.Combine(path, $"{Guid.NewGuid()}.{fileType}");
            using var writer = new BinaryWriter(File.OpenWrite(filePath));
            writer.Write(bytes);
            return await Task.FromResult(filePath);
        }
        //protected async Task<int?> FindWorkBreakdownStructureId(string code)
        //{
        //    if (code is null)
        //        throw new ArgumentException($"مقدار کد WBS نمی تواند تهی باشد");

        //    var workBreakdownStructure = await _context.WorkBreakdownStructures.FirstOrDefaultAsync(x => x.Code == code);
        //    return workBreakdownStructure?.Id;
        //}
    }
}
