using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Items.Data.Model
{
    [Index(nameof(Name), nameof(CreatedOn), IsUnique = true)]
    public class Item
    {
        private const int idMaxLength = 12;
        private const int nameMaxLength = 200;

        [Key]
        [MaxLength(idMaxLength)]
        public string Id { get; set; }
        [MaxLength(nameMaxLength)]
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ColorVersionId { get; set; }
        public Color Color { get; set; } = null!;

        public Item(string name, string note, Guid colorVersionId)
        {
            string guidAsBase64 = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Id = guidAsBase64[..idMaxLength].Replace("/", "1"); // forward slash is valid base64 character but will break api calls if used in URLs
            Name = name;
            Note = note;
            CreatedOn = DateTime.UtcNow;
            ColorVersionId = colorVersionId;
        }
    }
}
