using Application.DTO;
using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArchimeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("RastaCorsPolicy")]
    public class ArchiveItemController : ControllerBase
    {
        private readonly IArchiveItemService _ItemService;
        public ArchiveItemController(IArchiveItemService archiveItemService)
        {
            this._ItemService = archiveItemService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetItem([FromQuery]long id)
        {
            var result = await this._ItemService.GetArchiveItem(id);
            return File(result.Data.FileBuffer, result.Data.MimeType, result.Data.FileName);
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostItem(IFormFile file, [FromForm]SaveArchiveRequestModel item)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.OpenReadStream().CopyTo(memoryStream);
            var result = this._ItemService.PostArchiveItem(memoryStream, new ArchiveItem() { 
                CategoryId= item.CategoryId,
                CreateDate= DateTime.Now,   
                FileExtension = Path.GetExtension(file.FileName).Replace(".", ""),
                FileMimeType = file.ContentType,
                IsZiped = false,
                ParentId= item.ParentId,
                Size = file.Length,
                Title= item.Title,
                ProjectId= item.ProjectId,
                Type= "file"
            });
            return Ok(result);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetByParent([FromQuery]long? parentId, long projectId)
        {
            var result = await this._ItemService.GetByParent(parentId, projectId);
            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateFolder([FromBody] SaveArchiveRequestModel item)
        {
            var result = this._ItemService.CreateFolder(new ArchiveItem() { 
                CategoryId= item.CategoryId,
                CreateDate= DateTime.Now,
                IsZiped= false,
                ParentId= item.ParentId,
                Title= item.Title
            });

            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> SetDefaultArchive(SetDefaultArchiveRequestModel model)
        {
            var result = await this._ItemService.SetDefaultArchive(model);

            return Ok(result);
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteArchive([FromQuery]long id)
        {
            var result = await this._ItemService.DeleteArchiveItem(id);

            return Ok(result);
        }
    }
}
