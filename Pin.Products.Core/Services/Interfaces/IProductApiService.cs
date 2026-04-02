using Pin.Products.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin.Products.Core.Services.Interfaces
{
    public interface IProductApiService
    {
        Task<ResultModel<IEnumerable<ProductModel>>> GetAllAsync();
        Task<ResultModel<ProductModel>> CreateAsync(CreateOrUpdateProductModel newProduct);
        Task<ResultModel<ProductModel>> UpdateAsync(CreateOrUpdateProductModel newProduct);
        Task<bool> DeleteAsync(int id);
    }
}
