using Microsoft.Extensions.Logging;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Core.Services.Models;
using System.Net.Http.Json;

namespace Pin.Products.Core.Services
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CategoryApiService> _logger;

        public CategoryApiService(HttpClient httpClient, ILogger<CategoryApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ResultModel<CategoryModel>> CreateAsync(CreateOrUpdateCategoryModel category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category.Image))
                {
                    category.Image = "https://placeimg.com/640/480/any";
                }

                HttpResponseMessage result = await _httpClient.PostAsJsonAsync(string.Empty, category);
                if (result.IsSuccessStatusCode)
                {
                    CategoryModel? data = await result.Content.ReadFromJsonAsync<CategoryModel>();
                    if (data is not null)
                    {
                        return new ResultModel<CategoryModel> { Data = data };
                    }
                }

                ResultModel<CategoryModel> errorResult = new ResultModel<CategoryModel>();
                errorResult.Errors.Add("Category not created!");
                return errorResult;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error creating category");
                ResultModel<CategoryModel> errorResult = new ResultModel<CategoryModel>();
                errorResult.Errors.Add("Connection error, please try again later.");
                return errorResult;
            }
        }

        public async Task<ResultModel<IEnumerable<CategoryModel>>> GetAllAsync()
        {
            ResultModel<IEnumerable<CategoryModel>> resultModel = new ResultModel<IEnumerable<CategoryModel>>();
            try
            {
                IEnumerable<CategoryModel>? data = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryModel>>(string.Empty);
                if (data is not null)
                {
                    resultModel.Data = data;
                    return resultModel;
                }

                resultModel.Errors.Add("Something went wrong!");
                return resultModel;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error fetching categories");
                resultModel.Errors.Add("Connection error, please try again later.");
                return resultModel;
            }
        }

        public async Task<ResultModel<bool>> DeleteAsync(int id)
        {
            ResultModel<bool> resultModel = new ResultModel<bool>();
            try
            {
                HttpResponseMessage result = await _httpClient.DeleteAsync($"{id}");
                if (result.IsSuccessStatusCode)
                {
                    resultModel.Data = true;
                    return resultModel;
                }

                resultModel.Errors.Add("Category not deleted!");
                return resultModel;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error deleting category {Id}", id);
                resultModel.Errors.Add("Connection error, please try again later.");
                return resultModel;
            }
        }

        public async Task<ResultModel<CategoryModel>> UpdateAsync(CreateOrUpdateCategoryModel category)
        {
            try
            {
                HttpResponseMessage result = await _httpClient.PutAsJsonAsync($"{category.Id}", category);
                if (result.IsSuccessStatusCode)
                {
                    CategoryModel? data = await result.Content.ReadFromJsonAsync<CategoryModel>();
                    if (data is not null)
                    {
                        return new ResultModel<CategoryModel> { Data = data };
                    }
                }

                ResultModel<CategoryModel> errorResult = new ResultModel<CategoryModel>();
                errorResult.Errors.Add("Category not updated!");
                return errorResult;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error updating category {Id}", category.Id);
                ResultModel<CategoryModel> errorResult = new ResultModel<CategoryModel>();
                errorResult.Errors.Add("Connection error, please try again later.");
                return errorResult;
            }
        }
    }
}
