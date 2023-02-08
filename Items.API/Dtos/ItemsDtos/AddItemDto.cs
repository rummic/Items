namespace Items.API.Dtos.ItemsDtos
{
    public class AddItemDto
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public Guid ColorVersionId { get; set; }
    }
}
