using Pin.Products.Core.Services.Models;

namespace Pin.Products.Core.Services.Interfaces
{
    public interface ICategoryApiService
    {
        Task<ResultModel<IEnumerable<CategoryModel>>> GetAllAsync();
        Task<ResultModel<CategoryModel>> CreateAsync(CreateOrUpdateCategoryModel category);
        Task<ResultModel<CategoryModel>> UpdateAsync(CreateOrUpdateCategoryModel category);
        Task<ResultModel<bool>> DeleteAsync(int id);
    }
}
