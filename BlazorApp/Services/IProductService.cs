namespace BlazorApp.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using MinimalAPIERP.Dtos;

    public interface IProductService 
    {


        public Task<IList<ProductDtoView>> GetProductsPagedAsync(int page, int pageSize);
      
    }

}
