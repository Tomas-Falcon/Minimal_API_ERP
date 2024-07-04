using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP;

public partial class Product
{
    public int ProductId { get; set; }

    public Guid ProductIdGuid { get; set; } 

    public string SkuNumber { get; set; } = null!;

    public int CategoryId { get; set; }

    public int RecommendationId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal SalePrice { get; set; }

    public string? ProductArtUrl { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Created { get; set; } = DateTime.Now;

    public string ProductDetails { get; set; } = null!;

    public int Inventory { get; set; }

    public int LeadTime { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Raincheck> Rainchecks { get; set; } = new List<Raincheck>();
}
