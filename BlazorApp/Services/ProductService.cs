namespace BlazorApp.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using MinimalAPIERP.Dtos;

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<ProductDtoView>> GetProductsPagedAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<IList<ProductDtoView>>($"/product/paged?page={page}&pageSize={pageSize}");
            return response ?? new List<ProductDtoView>();
        }
    }

}
