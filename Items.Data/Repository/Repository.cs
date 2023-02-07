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

        public async Task<Color> AddColor(Color color)
        {
            var addedColor = await _dbContext.Colors.AddAsync(color);
            await _dbContext.SaveChangesAsync();
            return addedColor.Entity;
        }

        public async Task<Color> EditColor(Color colorToEdit, Color newColor)
        {
            var colorFromDb = await _dbContext.Colors.SingleAsync(x => x.VersionId == colorToEdit.VersionId);
            colorFromDb.IsActive = false;
            var addedColor = await _dbContext.Colors.AddAsync(newColor);
            await _dbContext.SaveChangesAsync();
            return addedColor.Entity;
        }

        public async Task<List<Color>> GetColors(Func<Color, bool> condition)
        {
            return (await _dbContext.Colors.ToListAsync()).Where(x => condition(x)).ToList();
        }
    }
}
