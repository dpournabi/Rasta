using Rasta.ProjectManagment.Application.Common.FileReaders.Providers;
using Rasta.ProjectManagment.Application.Common.Interfaces;

namespace Rasta.ProjectManagment.Application.Common.FileReaders
{
    public interface IFileReaderFactory
    {
        Task<FileReaderAbstraction> Get(FileReaderTypes readerType);
    }
    public class FileReaderFactory : IFileReaderFactory
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDateTimeService _dateTimeService;
        public FileReaderFactory(IApplicationDbContext applicationDbContext, IDateTimeService dateTimeService)
        {
            _applicationDbContext = applicationDbContext;
            _dateTimeService = dateTimeService;
        }
        public async Task<FileReaderAbstraction> Get(FileReaderTypes readerType) 
        {
            FileReaderAbstraction fileReaderAbstraction = null;

            switch (readerType)
            {
                //case FileReaderTypes.ExcelReader:
                //    fileReaderAbstraction = new ExcelReader(_applicationDbContext, _dateTimeService);
                //    break;
                case FileReaderTypes.MppReader:
                    fileReaderAbstraction = new MsProjectReader(_applicationDbContext, _dateTimeService);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return await Task.FromResult(fileReaderAbstraction);
        }
    }
}
