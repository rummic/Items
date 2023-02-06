using System.ComponentModel.DataAnnotations;

namespace Items.Data.Model
{
    public class Color
    {
        [Key]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
