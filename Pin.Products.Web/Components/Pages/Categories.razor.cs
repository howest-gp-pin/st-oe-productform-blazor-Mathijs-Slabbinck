using Microsoft.AspNetCore.Components;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Core.Services.Models;

namespace Pin.Products.Web.Components.Pages
{
    public partial class Categories
    {
        [Inject]
        private ICategoryApiService CategoryApiService { get; set; } = default!;

        private List<string>? errorMessages;
        private List<string>? successMessages;
        private CreateOrUpdateCategoryModel? newCategory;
        private IEnumerable<CategoryModel>? categoryModels;

        protected override async Task OnInitializedAsync()
        {
            await GetCategories();
        }

        private async Task GetCategories()
        {
            errorMessages = null;
            categoryModels = null;
            ResultModel<IEnumerable<CategoryModel>> result = await CategoryApiService.GetAllAsync();
            if (result.IsSuccess)
            {
                categoryModels = result.Data;
            }
            else
            {
                errorMessages = result.Errors;
            }
        }

        private void NewCategory()
        {
            errorMessages = null;
            newCategory = new CreateOrUpdateCategoryModel { Name = string.Empty, Image = string.Empty };
        }

        private async Task Save(CreateOrUpdateCategoryModel createOrUpdateCategoryModel)
        {
            ResultModel<CategoryModel> result;
            if (createOrUpdateCategoryModel.Id == 0)
            {
                result = await CategoryApiService.CreateAsync(createOrUpdateCategoryModel);
            }
            else
            {
                result = await CategoryApiService.UpdateAsync(createOrUpdateCategoryModel);
            }

            if (result.IsSuccess)
            {
                if (createOrUpdateCategoryModel.Id == 0)
                {
                    successMessages = new List<string> { "Category created" };
                }
                else
                {
                    successMessages = new List<string> { "Category updated" };
                }
            }
            else
            {
                errorMessages = result.Errors;
            }

            await GetCategories();
            newCategory = null;
        }

        private void Cancel()
        {
            newCategory = null;
        }

        private async Task Delete(int id)
        {
            errorMessages = null;
            ResultModel<bool> result = await CategoryApiService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                successMessages = new List<string> { "Category deleted" };
            }
            else
            {
                errorMessages = result.Errors;
            }

            await GetCategories();
        }

        private void Edit(CategoryModel categoryModel)
        {
            newCategory = new CreateOrUpdateCategoryModel
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Image = categoryModel.Image
            };
        }
    }
}
