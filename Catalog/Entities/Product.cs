using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
