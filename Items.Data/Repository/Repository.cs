using Items.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Items.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly ItemsDbContext _dbContext;

        public Repository(ItemsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Color
        public async Task<Color> AddColor(Color color)
        {
            var addedColor = await _dbContext.Colors.AddAsync(color);
            await _dbContext.SaveChangesAsync();
            return addedColor.Entity;
        }
        public async Task<List<Color>> GetColors(Func<Color, bool> condition)
        {
            return (await _dbContext.Colors.AsNoTracking().ToListAsync()).Where(condition).ToList();
        }

        public async Task<Color> EditColor(Color colorToEdit, Color newColor)
        {
            var colorFromDb = await _dbContext.Colors.SingleAsync(x => x.VersionId == colorToEdit.VersionId);
            colorFromDb.IsActive = false;
            var addedColor = await _dbContext.Colors.AddAsync(newColor);
            await _dbContext.SaveChangesAsync();
            return addedColor.Entity;
        }
        #endregion

        #region Item

        public async Task<Item> AddItem(Item item)
        {
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            var addedItem = await _dbContext.Items.Include(x => x.Color).SingleAsync(x => x.Id == item.Id);
            return addedItem;
        }

        public async Task<List<Item>> GetItems()
        {
            var items = await _dbContext.Items.AsNoTracking().ToListAsync();
            return items;
        }

        public async Task<Item> GetItem(string id)
        {
            var item = await _dbContext.Items.Include(x => x.Color).SingleOrDefaultAsync(x => x.Id == id);
            return item!;
        }

        public async Task<Item> EditItem(Item item)
        {
            var editedItem = _dbContext.Items.Update(item).Entity;
            await _dbContext.SaveChangesAsync();
            return editedItem;
        }

        public async Task<List<Item>> GetItemsPaged(string query, bool ascending, DateTime lastCreatedOn, int pageSize)
        {
            var items = _dbContext.Items.AsNoTracking().Include(x => x.Color).Where(x => x.Name.Contains(query) ||
                                                                          x.Note.Contains(query) ||
                                                                          x.Color.Name.Contains(query));
            //Keyset pagination
            if (ascending) return await items.OrderBy(x => x.CreatedOn).Where(x => x.CreatedOn > lastCreatedOn).Take(pageSize).ToListAsync();
            else return await items.OrderByDescending(x => x.CreatedOn).Where(x => x.CreatedOn < lastCreatedOn).Take(pageSize).ToListAsync();
        }

        #endregion
    }
}
