using BlazorApp1.Dtos;

namespace BlazorApp1.Services
{
    public interface ICartItemService
    {
        Task<IList<CartItemDto>> GetCartItemsPagedAsync(int page, int pageSize);
    }
}