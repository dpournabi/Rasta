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
    public class ArchiveItemService : IArchiveItemService
    {
        private readonly MyDbContext _DbContext;
        private readonly IMinIoHelperService _MinIo;
        public ArchiveItemService(MyDbContext dbContext, IMinIoHelperService minIo)
        {
            this._DbContext = dbContext;
            this._MinIo = minIo;
        }

        public async ValueTask<CommonResult<SaveArchiveResponseModel>> CreateFolder(ArchiveItem item)
        {
            item.Type = "folder";
            this._DbContext.ArchiveItems.Add(item);
            await this._DbContext.SaveChangesAsync();

            return CommonResult<SaveArchiveResponseModel>.Success("Ok", new SaveArchiveResponseModel()
            {
                ArchiveId = item.Id,
                Url = ""
            });
        }

        public async ValueTask<CommonResult<bool>> DeleteArchiveItem(long id)
        {
            var archive = this._DbContext.ArchiveItems.Where(a => a.Id == id).FirstOrDefault();
            if (archive != null)
            {
                archive.DeleteDate = DateTime.Now;
                this._DbContext.Update(archive);
                await this._DbContext.SaveChangesAsync();

                return CommonResult<bool>.Success("Ok", true);
            }
            else
            {
                return await ValueTask.FromResult(CommonResult<bool>.Failure("Not Found!", null, false));
            }
        }

        public async ValueTask<CommonResult<GetArchiveItemResponseModel>> GetArchiveItem(long id)
        {
            var archive = await this._DbContext.ArchiveItems.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (archive == null)
            {
                return CommonResult<GetArchiveItemResponseModel>.Failure("Not Found!", null, null);
            }
            else
            {
                ///get archive from minio ..........................
                ///
                var stream = await this._MinIo.Download(archive.MinIoUrl.Split('/')[0], archive.MinIoUrl.Split('/')[1]);
                GetArchiveItemResponseModel result = new GetArchiveItemResponseModel();
                result.FileBuffer = stream.ToArray();
                result.FileName = archive.Title;
                result.MimeType = archive.FileMimeType;

                return CommonResult<GetArchiveItemResponseModel>.Success("Ok", result);
            }
        }

        public async ValueTask<CommonResult<IEnumerable<ArchiveItem>>> GetByParent(long? parentId, long projectId)
        {
            var icons = await this._DbContext.ArchiveItemSettings.ToListAsync();
            var result = await this._DbContext.ArchiveItems
                .Where(item => item.ParentId == parentId && item.ProjectId == projectId)
                .ToListAsync();

            long[] ids = result.Select(item => item.Id).ToArray();

            var defaults = this._DbContext.DefaultArchives.Where(d => d.ArchiveId.HasValue && ids.Contains(d.ArchiveId.Value));

            foreach (var item in result)
            {
                var icon = icons.Where(i => i.FileExtension == item.FileExtension).FirstOrDefault();
                if (icon == null)
                {
                    item.Icon = "<i></i>";
                }
                else
                {
                    item.Icon = icon.Icon;
                }

                item.IsDefault = defaults.Any(d => d.ArchiveId == item.Id);
            }

            return CommonResult<IEnumerable<ArchiveItem>>.Success("Ok", result);
        }

        public async ValueTask<CommonResult<SaveArchiveResponseModel>> PostArchiveItem(MemoryStream stream, ArchiveItem item)
        {
            bool isWhiteList = await this._DbContext.ArchiveItemSettings.AnyAsync(s =>
                s.FileExtension != null
                && item.FileExtension != null && s.FileExtension == item.FileExtension.Replace(".", "")
            );
            if (!isWhiteList)
            {
                return CommonResult<SaveArchiveResponseModel>.Failure($"Invalid Extension! ({item.FileExtension})", null, null);
            }
            var category = this._DbContext.Categories.Where(c => c.Id == item.CategoryId).FirstOrDefault();
            if (category == null)
            {
                return CommonResult<SaveArchiveResponseModel>.Failure($"Invalid CategoryId! ({item.CategoryId})", null, null);
            }
            int used = this._MinIo.CalculateBucketSize($"bucket{item.CategoryId}").GetAwaiter().GetResult();
            if (used + stream.Length > category.MaxCapacity)
            {
                return CommonResult<SaveArchiveResponseModel>
                    .Failure($"Haven't enough storage! (Max Capacity: {item.CategoryId})", null, null);
            }
            //post to minio
            stream.Position = 0;
            string minio_url = await this._MinIo.Upload(stream, item);
            //save the archive on database
            item.MinIoUrl = minio_url;
            this._DbContext.ArchiveItems.Add(item);
            await this._DbContext.SaveChangesAsync();

            return CommonResult<SaveArchiveResponseModel>.Success("Ok", new SaveArchiveResponseModel()
            {
                ArchiveId = item.Id,
                Url = minio_url
            });
        }

        public async ValueTask<CommonResult<bool>> SetDefaultArchive(SetDefaultArchiveRequestModel model)
        {
            var defaultArchive = this._DbContext.DefaultArchives
                .Where(d => d.CategoryId == model.CategoryId && d.ProjectId == model.ProjectId).FirstOrDefault();
            if (defaultArchive == null)
            {
                //insert default
                this._DbContext.DefaultArchives.Add(new DefaultArchive()
                {
                    ProjectId = model.ProjectId,
                    CategoryId = model.CategoryId,
                    ArchiveId = model.ArchiveId
                });
                await this._DbContext.SaveChangesAsync();
            }
            else
            {
                //update default
                defaultArchive.ArchiveId = model.ArchiveId;
                this._DbContext.Update(defaultArchive);
                await this._DbContext.SaveChangesAsync();
            }
            return CommonResult<bool>.Success("Ok", true);

        }
    }
}
