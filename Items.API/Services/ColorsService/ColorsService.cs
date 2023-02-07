using Items.API.Dtos;
using Items.Data.Model;
using Items.Data.Repository;

namespace Items.API.Services.ColorsService
{
    public class ColorsService : IColorsService
    {
        private readonly IRepository _repository;

        public ColorsService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResponseDto<List<Color>>> GetColors(Func<Color, bool> condition)
        {
            var response = new ResponseDto<List<Color>>();
            List<Color> colors = await _repository.GetColors(condition);

            if (!colors.Any())
            {
                response.AddError("There are no colors.");
                return response;
            }
            response.Value = colors;
            return response;
        }

        public async Task<ResponseDto<Color>> AddColor(string colorName)
        {
            var response = new ResponseDto<Color>();
            var existingColor = await _repository.GetColors(x => x.Name == colorName && x.IsActive);
            if (existingColor.Any())
            {
                response.AddError($"Color {colorName} already exists.");
                return response;
            }

            var color = new Color(colorName);
            var addResult = await _repository.AddColor(color);
            response.Value = addResult;
            return response;
        }

        public async Task<ResponseDto<Color>> EditColor(Guid versionId, string colorName)
        {
            var response = new ResponseDto<Color>();
            var colorsFound = await _repository.GetColors(x => x.VersionId == versionId);
            if (!colorsFound.Any())
            {
                response.AddError($"No color with {versionId} found.");
            }

            var colorToEdit = colorsFound.Single();
            Guid colorId = colorToEdit.ColorId;
            var newColor = new Color(colorId, colorName);
            var editResult = await _repository.EditColor(colorToEdit, newColor);
            response.Value = editResult;
            return response;
        }
    }
}
