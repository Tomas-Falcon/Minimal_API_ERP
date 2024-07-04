
namespace ERP;

public partial class Category
{
    public int CategoryId { get; set; }

    public Guid CategoryIdGuid { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
