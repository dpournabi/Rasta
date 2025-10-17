using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMinIoHelperService
    {
        ValueTask<MemoryStream> Download(string bucketId, string fileName);
        ValueTask<string> Upload(MemoryStream stream, Infrastructure.ArchiveItem item);
        ValueTask<int> CalculateBucketSize(string bucketId);        
    }
}
