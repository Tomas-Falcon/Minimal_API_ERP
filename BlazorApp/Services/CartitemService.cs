namespace BlazorApp.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using MinimalAPIERP.Dtos;

    public class CartItemService : ICartItemService
    {
        private readonly HttpClient _httpClient;

        public CartItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<CartItemDtoView>> GetCartItemsPagedAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<IList<CartItemDtoView>>($"/cartitem/paged?page={page}&pageSize={pageSize}");
            return response ?? new List<CartItemDtoView>();
        }
    }

}
