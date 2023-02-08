namespace Items.API.Dtos.ItemsDtos
{
    public class EditItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public Guid ColorVersionId { get; set; }
    }
}
