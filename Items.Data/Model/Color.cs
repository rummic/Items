using System.ComponentModel.DataAnnotations;

namespace Items.Data.Model
{
    public class Color
    {
        /// <summary>
        /// Unique identifier of the <see cref="Color"/> entity
        /// </summary>
        [Key]
        public Guid VersionId { get; set; }

        /// <summary>
        /// Identifier of the group of the versions of <see cref="Color"/>
        /// </summary>
        [Required]
        public Guid ColorId { get; set; }

        /// <summary>
        /// Name of the <see cref="Color"/>
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Flag determining active version of the <see cref="Color"/>
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Creates new <see cref="Color"/> instance. 
        /// Guids <see cref="VersionId"/> and <see cref="ColorId"/> are randomly generated.
        /// <see cref="IsActive"/> is set to true.
        /// </summary>
        /// <param name="name"></param>
        public Color(string name)
        {
            ColorId = Guid.NewGuid();
            VersionId = Guid.NewGuid();
            Name = name;
            IsActive = true;
        }

        /// <summary>
        /// Creates new <see cref="Color"/> instance with already existing <see cref="ColorId"/>. Used to create new versions of the <see cref="Color"/>.
        /// Guid <see cref="VersionId"/> is randomly generated.
        /// <see cref="IsActive"/> is set to true.
        /// </summary>
        /// <param name="colorId"></param>
        /// <param name="name"></param>
        public Color(Guid colorId, string name)
        {
            ColorId = colorId;
            VersionId = Guid.NewGuid();
            Name = name;
            IsActive = true;
        }

    }
}
