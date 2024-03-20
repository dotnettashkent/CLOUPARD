using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Features
{
    [Table("products")]
    public class ProductEntity : BaseEntity
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
