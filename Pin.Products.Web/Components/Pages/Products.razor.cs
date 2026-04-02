using Microsoft.AspNetCore.Components;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Core.Services.Models;

namespace Pin.Products.Web.Components.Pages
{
    public partial class Products
    {
        [Inject]
        private IProductApiService ProductService { get; set; } = default!;

        [Inject]
        private ICategoryApiService CategoryService { get; set; } = default!;

        private List<string>? errorMessages;
        private List<string>? successMessages;
        private CreateOrUpdateProductModel? newProduct;
        private IEnumerable<ProductModel>? productModels;
        private IEnumerable<CategoryModel> categories = Enumerable.Empty<CategoryModel>();

        protected override async Task OnInitializedAsync()
        {
            Task<ResultModel<IEnumerable<ProductModel>>> productTask = ProductService.GetAllAsync();
            Task<ResultModel<IEnumerable<CategoryModel>>> categoryTask = CategoryService.GetAllAsync();

            await Task.WhenAll(productTask, categoryTask);

            List<string> errors = new List<string>();

            ResultModel<IEnumerable<ProductModel>> productResult = productTask.Result;
            if (productResult.IsSuccess)
            {
                productModels = productResult.Data;
            }
            else
            {
                errors.AddRange(productResult.Errors);
            }

            ResultModel<IEnumerable<CategoryModel>> categoryResult = categoryTask.Result;
            if (categoryResult.IsSuccess)
            {
                categories = categoryResult.Data ?? Enumerable.Empty<CategoryModel>();
            }
            else
            {
                errors.AddRange(categoryResult.Errors);
            }

            if (errors.Any())
            {
                errorMessages = errors;
            }
        }

        private async Task GetProducts()
        {
            errorMessages = null;
            productModels = null;
            ResultModel<IEnumerable<ProductModel>> result = await ProductService.GetAllAsync();
            if (result.IsSuccess)
            {
                productModels = result.Data;
            }
            else
            {
                errorMessages = result.Errors;
            }
        }

        private void NewProduct()
        {
            errorMessages = null;
            newProduct = new CreateOrUpdateProductModel
            {
                Title = string.Empty,
                Description = string.Empty,
                Images = new List<string>()
            };
        }

        private async Task Save(CreateOrUpdateProductModel createOrUpdateProductModel)
        {
            ResultModel<ProductModel> result;
            if (createOrUpdateProductModel.Id == 0)
            {
                result = await ProductService.CreateAsync(createOrUpdateProductModel);
            }
            else
            {
                result = await ProductService.UpdateAsync(createOrUpdateProductModel);
            }

            if (result.IsSuccess)
            {
                if (createOrUpdateProductModel.Id == 0)
                {
                    successMessages = new List<string> { "Product created" };
                }
                else
                {
                    successMessages = new List<string> { "Product updated" };
                }
            }
            else
            {
                errorMessages = result.Errors;
            }

            await GetProducts();
            newProduct = null;
        }

        private void Cancel()
        {
            newProduct = null;
        }

        private async Task Delete(int id)
        {
            errorMessages = null;
            ResultModel<bool> result = await ProductService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                successMessages = new List<string> { "Product deleted" };
            }
            else
            {
                errorMessages = result.Errors;
            }

            await GetProducts();
        }

        private void Edit(ProductModel productModel)
        {
            newProduct = new CreateOrUpdateProductModel
            {
                Id = productModel.Id,
                Title = productModel.Title,
                Price = productModel.Price,
                Description = productModel.Description,
                CategoryId = productModel.Category?.Id ?? 0,
                Images = productModel.Images
            };
        }
    }
}
