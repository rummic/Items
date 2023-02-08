using Items.API.Dtos;
using Items.API.Dtos.ItemsDtos;
using Items.Data.Model;
using Items.Data.Repository;

namespace Items.API.Services.ItemsServices
{
    public class ItemsService : IItemsService
    {
        private IRepository _repository;

        public ItemsService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<Item>> AddItem(AddItemDto itemDto)
        {
            var response = new ResponseDto<Item>();
            var newItem = new Item(itemDto.Name, itemDto.Note, itemDto.colorVersionId);
            Item addedItem;
            try
            {
                addedItem = await _repository.AddItem(newItem);
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
                return response;
            }

            response.Value = addedItem;
            return response;
        }

        public async Task<ResponseDto<Item>> EditItem(EditItemDto itemDto)
        {
            var response = new ResponseDto<Item>();
            var itemToEdit = await _repository.GetItem(itemDto.Id);
            if (itemToEdit == null)
            {
                response.AddError($"No {itemDto.Id} item exists.");
                return response;
            }

            itemToEdit.Name = itemDto.Name;
            itemToEdit.Note = itemDto.Note;
            itemToEdit.ColorVersionId = itemDto.colorVersionId;
            var editResult = await _repository.EditItem(itemToEdit);
            response.Value = editResult;
            return response;
        }

        public async Task<ResponseDto<List<Item>>> GetItems()
        {
            var response = new ResponseDto<List<Item>>();
            var items = await _repository.GetItems();
            if(!items.Any())
            {
                response.AddError("There are no items.");
                return response;
            }

            response.Value = items;
            return response;
        }
        public async Task<ResponseDto<Item>> GetItem(string id)
        {
            var response = new ResponseDto<Item>();
            var item = await _repository.GetItem(id);
            if (item == null)
            {
                response.AddError($"There is no {id} items.");
                return response;
            }

            response.Value = item;
            return response;
        }

        public async Task<ResponseDto<ItemsPagedDto>> GetItemsPaged(SearchItemsDto searchDto)
        {
            var response = new ResponseDto<ItemsPagedDto>();
            var items = await _repository.GetItemsPaged(searchDto.Ascending, searchDto.LastCreatedOn, searchDto.PageSize);
            if (!items.Any())
            {
                response.AddError("There are no items.");
                return response;
            }
            var pagingResult = new ItemsPagedDto(items, items.Last().CreatedOn);
            response.Value = pagingResult;
            return response;
        }
    }
}
