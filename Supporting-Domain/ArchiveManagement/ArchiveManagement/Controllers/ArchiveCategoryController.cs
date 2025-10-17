using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArchimeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("RastaCorsPolicy")]
    public class ArchiveCategoryController : ControllerBase
    {
        private readonly IArchiveCategoryService _CategoryService;
        public ArchiveCategoryController(IArchiveCategoryService archiveCategoryService)
        {
            this._CategoryService = archiveCategoryService;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetCategories([FromQuery]ReadCategoryModel model)
        {
            var result = await this._CategoryService.GetCategories(model);

            return Ok(result);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetDefaultArchive([FromQuery] int categoryId, [FromQuery] int projectId)
        {
            var result = await this._CategoryService.GetDefaultArchive(categoryId, projectId);

            return Ok(result);
        }
    }
}
