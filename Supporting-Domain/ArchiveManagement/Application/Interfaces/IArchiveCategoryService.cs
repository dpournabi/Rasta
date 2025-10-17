using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArchiveCategoryService
    {
        ValueTask<CommonResult<IEnumerable<DTO.ReadCategoryResponseModel>>> GetCategories(DTO.ReadCategoryModel model);
        ValueTask<CommonResult<long>> GetDefaultArchive(int categoryId, int projectId);
    }
}
