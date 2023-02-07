using Items.Data.Model;

namespace Items.Data.Repository
{
    public interface IRepository
    {
        public Task<Color> AddColor(Color color);
        public Task<List<Color>> GetColors(Func<Color, bool> condition);
        public Task<Color> EditColor(Color colorToEdit, Color newColor);
    }
}
