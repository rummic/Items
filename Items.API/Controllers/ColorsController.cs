using Items.API.Services.ColorsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Items.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColorsController : ControllerBase
    {
        private readonly IColorsService _colorsService;
        public ColorsController(IColorsService colorsService)
        {
            _colorsService = colorsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetActive()
        {
            var response = await _colorsService.GetColors(x => x.IsActive);
            if (response.HasErrors)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{colorId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetColorsByColorId(Guid colorId)
        {
            var response = await _colorsService.GetColors(x => x.ColorId == colorId);
            if (response.HasErrors)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] string colorName)
        {
            var response = await _colorsService.AddColor(colorName);
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{versionId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditColor(Guid versionId, [FromBody] string colorName)
        {
            var response = await _colorsService.EditColor(versionId, colorName);
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

}
