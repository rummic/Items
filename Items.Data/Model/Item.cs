using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Items.Data.Model
{
    [Index(nameof(Name), IsUnique = true)]
    public class Item
    {
        private const int idMaxLength = 12;
        private const int nameMaxLength = 200;

        [Key]
        [MaxLength(idMaxLength)]
        public string Id { get; set; }
        [MaxLength(nameMaxLength)]
        public string Name { get; set; }
        
        public Guid ColorVersionId { get; set; }
        public Color Color { get; set; } = null!;

        public Item(string name, Guid colorVersionId)
        {
            string guidAsBase64 = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Id = guidAsBase64[..idMaxLength];
            Name = name;
            ColorVersionId = colorVersionId;
        }
    }
}
