using Microsoft.Extensions.Logging;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Core.Services.Models;
using System.Net.Http.Json;

namespace Pin.Products.Core.Services
{
    public class ProductApiService : IProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductApiService> _logger;

        public ProductApiService(HttpClient httpClient, ILogger<ProductApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ResultModel<ProductModel>> CreateAsync(CreateOrUpdateProductModel product)
        {
            try
            {
                HttpResponseMessage result = await _httpClient.PostAsJsonAsync(string.Empty, product);
                if (result.IsSuccessStatusCode)
                {
                    ProductModel? data = await result.Content.ReadFromJsonAsync<ProductModel>();
                    if (data is not null)
                    {
                        return new ResultModel<ProductModel> { Data = data };
                    }
                }

                ResultModel<ProductModel> errorResult = new ResultModel<ProductModel>();
                errorResult.Errors.Add("Product not created!");
                return errorResult;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error creating product");
                ResultModel<ProductModel> errorResult = new ResultModel<ProductModel>();
                errorResult.Errors.Add("Connection error, please try again later.");
                return errorResult;
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

                resultModel.Errors.Add("Product not deleted!");
                return resultModel;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error deleting product {Id}", id);
                resultModel.Errors.Add("Connection error, please try again later.");
                return resultModel;
            }
        }

        public async Task<ResultModel<IEnumerable<ProductModel>>> GetAllAsync()
        {
            ResultModel<IEnumerable<ProductModel>> resultModel = new ResultModel<IEnumerable<ProductModel>>();
            try
            {
                IEnumerable<ProductModel>? data = await _httpClient.GetFromJsonAsync<IEnumerable<ProductModel>>(string.Empty);
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
                _logger.LogError(httpRequestException, "Error fetching products");
                resultModel.Errors.Add("Connection error, please try again later.");
                return resultModel;
            }
        }

        public async Task<ResultModel<ProductModel>> UpdateAsync(CreateOrUpdateProductModel product)
        {
            try
            {
                HttpResponseMessage result = await _httpClient.PutAsJsonAsync($"{product.Id}", product);
                if (result.IsSuccessStatusCode)
                {
                    ProductModel? data = await result.Content.ReadFromJsonAsync<ProductModel>();
                    if (data is not null)
                    {
                        return new ResultModel<ProductModel> { Data = data };
                    }
                }

                ResultModel<ProductModel> errorResult = new ResultModel<ProductModel>();
                errorResult.Errors.Add("Product not updated!");
                return errorResult;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error updating product {Id}", product.Id);
                ResultModel<ProductModel> errorResult = new ResultModel<ProductModel>();
                errorResult.Errors.Add("Connection error, please try again later.");
                return errorResult;
            }
        }
    }
}
