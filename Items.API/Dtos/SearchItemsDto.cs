namespace Items.API.Dtos
{
    public class SearchItemsDto
    {
        public int PageSize { get; set; }
        public DateTime LastCreatedOn {  get; set; }
        public bool Ascending { get; set; }
    }
}
