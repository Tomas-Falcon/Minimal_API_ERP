namespace BlazorApp1.Services
{
    using BlazorApp1.Dtos;
    using System.Threading.Tasks;

    public interface IProductService 
    {


        public Task<IList<ProductDtoView>> GetProductsPagedAsync(int page, int pageSize);
      
    }

}
