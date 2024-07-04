using ERP;

namespace MinimalAPIERP.Dtos
{
    public record RaincheckDto
    {
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreDto? Store { get; set; }
        public ProductDto? Product { get; set; }
    }

    public record StoreDtoView
    {
        public string? Name { get; set; }
        public required Guid GuidId { get; set; }
    }

    public record StoreDto
    {
        public string? Name { get; set; }
    }

    public record ProductDtoView
    {
        public required Guid ProductIdGuid { get; set; }
        public string SkuNumber { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string? ProductArtUrl { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ProductDetails { get; set; }
        public int Inventory { get; set; }
        public int LeadTime { get; set; }
        public Guid? CategoryIdGuid { get; set; }
    }

    public record ProductDto
    {
        public string SkuNumber { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string? ProductArtUrl { get; set; }
        public string Description { get; set; }
        public string ProductDetails { get; set; }
        public int Inventory { get; set; }
        public int LeadTime { get; set; }
        public Guid? CategoryIdGuid { get; set; }
    }


    public record CategoryDto
    {
        public required Guid CategoryIdGuid { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

   

    public record CategoryDtoView
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public record OrderDto
    {
        public required Guid OrderIdGuid { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Total { get; set; }
    }

    public record OrderDtoView
    {
        public DateTime OrderDate { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Total { get; set; }
    }

    public record OrderDetailDto
    {
        public required Guid OrderDetailIdGuid { get; set; }
        public required Guid OrderGuid { get; set; }
        public required Guid ProductGuid { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public record OrderDetailDtoView
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public record CartItemDto
    {
        public Guid CartItemIdGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public record CartItemDtoView
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }

}
