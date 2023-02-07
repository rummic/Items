using System.ComponentModel.DataAnnotations;

namespace Items.Data.Model
{
    public class Color
    {
        [Required]
        public Guid ColorId { get; set; }

        [Key]
        public Guid VersionId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public bool IsActive { get; set; }

        public Color(string name)
        {
            ColorId = Guid.NewGuid();
            VersionId = Guid.NewGuid();
            Name = name;
            IsActive = true;
        }
        public Color(Guid colorId, string name)
        {
            ColorId = colorId;
            VersionId = Guid.NewGuid();
            Name = name;
            IsActive = true;
        }

    }
}
