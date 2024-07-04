namespace BlazorApp1.Services
{
    using BlazorApp1.Dtos;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class CartItemService : ICartItemService
    {
        private readonly HttpClient _httpClient;

        public CartItemService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:54528")
            };
        }

        public async Task<IList<CartItemDto>> GetCartItemsPagedAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<IList<CartItemDto>>($"/cartitem/paged?page={page}&pageSize={pageSize}");
            return response ?? new List<CartItemDto>();
        }
    }

}
