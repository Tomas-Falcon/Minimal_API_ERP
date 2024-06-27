namespace MinimalAPIERP.Dtos
{
    public class RaincheckDto
    {
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreDto? Store { get; set; }
        public ProductDto? Product { get; set; }
    }

    public class StoreDtoView
    {
        public string? Name { get; set; }
        public Guid GuidId { get; set; }
    }

    public class StoreDto
    {
        public string? Name { get; set; }
    }

    public class ProductDtoView
    {
        public string? Name { get; set; }
        public Guid? CategoryIdGuid { get; set; }
        public Guid ProductIdGuid { get; set; }

    }
    public class ProductDto
    {

        public string? Name { get; set; }
        public Guid? CategoryIdGuid { get; set; }
    }

    public class CategoryDto
    {
        public Guid CategoryIdGuid { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

   

    public class CategoryDtoView
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class OrderDto
    {
        public Guid OrderIdGuid { get; set; }
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

    public class OrderDtoView
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

   
}
