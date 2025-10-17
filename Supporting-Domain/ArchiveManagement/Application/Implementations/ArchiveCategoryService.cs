using Application.DTO;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class ArchiveCategoryService : IArchiveCategoryService
    {
        private readonly MyDbContext _DbContext;
        private readonly IMinIoHelperService _MinIo;
        public ArchiveCategoryService(MyDbContext dbContext, IMinIoHelperService minIo)
        {
            this._DbContext = dbContext;
            this._MinIo = minIo;
        }
        public async ValueTask<CommonResult<IEnumerable<DTO.ReadCategoryResponseModel>>> GetCategories(ReadCategoryModel model)
        {
            var result = this._DbContext.Categories.AsQueryable();

            if (model.Id > 0)
            {
                result = result.Where(c => c.Id == model.Id);
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                result = result.Where(c => c.Name.Contains(model.Name));
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                result = result.Where(c => c.Description != null && c.Description.Contains(model.Description));
            }

            var final = (await result.ToListAsync()).Select(c => new ReadCategoryResponseModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Active = c.Active,
                CreateDate = c.CreateDate,
                UsedStoragePercent = this.CalculateCategorySize(c),
                MaxCapacity = c.MaxCapacity
            }).OrderBy(c => c.Id);

            return CommonResult<IEnumerable<DTO.ReadCategoryResponseModel>>.Success("Ok", final);
        }

        public async ValueTask<CommonResult<long>> GetDefaultArchive(int categoryId, int projectId)
        {
            var defaultArchive = await this._DbContext.DefaultArchives
                .Where(d => d.CategoryId== categoryId && d.ProjectId == projectId).FirstOrDefaultAsync();    
            if (defaultArchive != null && defaultArchive.ArchiveId.HasValue)
            {
                return CommonResult<long>.Success("Ok", defaultArchive.ArchiveId.Value);
            }
            return CommonResult<long>.Failure("Not Found!", null, 0);
        }

        private int CalculateCategorySize(Category category)
        {
            if (category.MaxCapacity > 0)
            {
                int used = this._MinIo.CalculateBucketSize($"bucket{category.Id}").GetAwaiter().GetResult();
                float percent = (used * 100 / category.MaxCapacity.Value);
                return (int)(percent);
            }
            else
            {
                return 0;
            }
        }
    }
}
