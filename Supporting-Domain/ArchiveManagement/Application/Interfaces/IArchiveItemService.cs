using Application.DTO;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArchiveItemService
    {
        ValueTask<CommonResult<GetArchiveItemResponseModel>> GetArchiveItem(long id);
        ValueTask<CommonResult<SaveArchiveResponseModel>> PostArchiveItem(MemoryStream stream, Infrastructure.ArchiveItem item);
        ValueTask<CommonResult<IEnumerable<ArchiveItem>>> GetByParent(long? parentId, long projectId);
        ValueTask<CommonResult<SaveArchiveResponseModel>> CreateFolder(ArchiveItem item);
        ValueTask<CommonResult<bool>> SetDefaultArchive(SetDefaultArchiveRequestModel model);
        ValueTask<CommonResult<bool>> DeleteArchiveItem(long id);
    }
}
