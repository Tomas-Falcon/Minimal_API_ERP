using MinimalAPIERP.Dtos;

namespace BlazorApp.Services
{
    public interface ICartItemService
    {
        Task<IList<CartItemDtoView>> GetCartItemsPagedAsync(int page, int pageSize);
    }
}