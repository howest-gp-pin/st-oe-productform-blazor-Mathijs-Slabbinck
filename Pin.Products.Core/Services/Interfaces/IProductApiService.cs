using Pin.Products.Core.Services.Models;

namespace Pin.Products.Core.Services.Interfaces
{
    public interface IProductApiService
    {
        Task<ResultModel<IEnumerable<ProductModel>>> GetAllAsync();
        Task<ResultModel<ProductModel>> CreateAsync(CreateOrUpdateProductModel product);
        Task<ResultModel<ProductModel>> UpdateAsync(CreateOrUpdateProductModel product);
        Task<ResultModel<bool>> DeleteAsync(int id);
    }
}
