namespace BlazorApp1.Services
{
    using BlazorApp1.Dtos;
    using Microsoft.Extensions.Configuration;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:54528")
            };
        }

        public async Task<IList<ProductDtoView>> GetProductsPagedAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<IList<ProductDtoView>>($"/erp/product/paged?page={page}&pageSize={pageSize}");
            return response ?? new List<ProductDtoView>();
        }
    }

}
