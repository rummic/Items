using Items.API.Dtos;
using Items.API.Dtos.ItemsDtos;
using Items.Data.Model;

namespace Items.API.Services.ItemsServices
{
    public interface IItemsService
    {
        public Task<ResponseDto<Item>> AddItem(AddItemDto itemDto);
        public Task<ResponseDto<Item>> EditItem(EditItemDto itemDto);
        public Task<ResponseDto<List<Item>>> GetItems();
        public Task<ResponseDto<Item>> GetItem(string id);
        public Task<ResponseDto<ItemsPagedDto>> GetItemsPaged(SearchItemsDto searchDto);
    }
}
