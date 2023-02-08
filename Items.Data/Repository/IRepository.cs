using Items.Data.Model;

namespace Items.Data.Repository
{
    // I have created the repository to act as a mockable layer above DbContext
    // and a single repository is used for both models,
    // as there is not enough logic here that would make the separation worth it.
    // If there would be a need to do repo per model it is easily splittable.
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
        public Task<List<Item>> GetItemsPaged(string query, bool ascending, DateTime lastCreatedOn, int pageSize);
        #endregion
    }
}
