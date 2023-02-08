using Items.API.Dtos;
using Items.API.Dtos.ItemsDtos;
using Items.API.Services.ItemsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Items.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            var response = await _itemsService.GetItems();
            if (response.HasErrors)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("search")]
        public async Task<ActionResult> GetItemsPaged([FromBody] SearchItemsDto searchDto)
        {
            var response = await _itemsService.GetItemsPaged(searchDto);
            if (response.HasErrors)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(string id)
        {
            var response = await _itemsService.GetItem(id);
            if (response.HasErrors)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddItem([FromBody] AddItemDto itemDto)
        {
            var response = await _itemsService.AddItem(itemDto);
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditItem([FromBody] EditItemDto itemDto)
        {
            var response = await _itemsService.EditItem(itemDto);
            if (response.HasErrors)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
