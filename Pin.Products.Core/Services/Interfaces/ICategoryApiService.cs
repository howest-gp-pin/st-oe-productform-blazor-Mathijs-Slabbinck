using Pin.Products.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin.Products.Core.Services.Interfaces
{
    public interface ICategoryApiService
    {
        Task<ResultModel<IEnumerable<CategoryModel>>> GetAllAsync();
        Task<ResultModel<CategoryModel>> CreateAsync(CreateOrUpdateCategoryModel newCategory);
        Task<ResultModel<CategoryModel>> UpdateAsync(CreateOrUpdateCategoryModel newCategory);
        Task<bool> DeleteAsync(int id);
    }
}
