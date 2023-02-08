using Items.Data.Model;

namespace Items.API.Dtos.ItemsDtos
{
    public class ItemsPagedDto
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public DateTime LastCreatedOn { get; set; }

        public ItemsPagedDto(List<Item> items, DateTime lastCreatedOn)
        {
            Items = items;
            LastCreatedOn = lastCreatedOn;
        }
    }
}
