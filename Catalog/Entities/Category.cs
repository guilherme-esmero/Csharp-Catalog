using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalog.Entities;

public class Category
{
    public Category()
    {
        Products = new List<Product>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<Product> Products { get; set; }
}
