using Items.Data.Model;

namespace Items.Data.Repository
{
    public interface IRepository
    {
        #region Color
        public Task<Color> AddColor(Color color);
        public Task<List<Color>> GetColors(Func<Color, bool> condition);
        public Task<Color> EditColor(Color colorToEdit, Color newColor);
        #endregion

        #region Item
        public Task<Item> AddItem(Item item);
        public Task<List<Item>> GetItems();
        public Task<Item> GetItem(string id);
        public Task<Item> EditItem(Item item);
        public Task<List<Item>> GetItemsPaged(bool ascending, DateTime lastCreatedOn, int pageSize);

        #endregion
    }
}
