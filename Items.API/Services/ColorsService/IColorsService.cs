using Items.API.Dtos;
using Items.Data.Model;

namespace Items.API.Services.ColorsService
{
    public interface IColorsService
    {
        public Task<ResponseDto<List<Color>>> GetColors(Func<Color, bool> condition);
        public Task<ResponseDto<Color>> AddColor(string colorName);
        public Task<ResponseDto<Color>> EditColor(Guid versionId, string colorName);
    }
}
