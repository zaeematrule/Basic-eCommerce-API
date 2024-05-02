using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTemplate.Core.Products;

public record Product(int? Id) : BaseEntity(Id)
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }
};
