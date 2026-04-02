using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pin.Products.Core.Services
{
    public class ProductApiService : IProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/products");
        }

        public async Task<ResultModel<ProductModel>> CreateAsync(CreateOrUpdateProductModel newProduct)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}", newProduct);
                if (result.IsSuccessStatusCode)
                {
                    return new ResultModel<ProductModel>
                    {
                        Data = JsonSerializer.Deserialize<ProductModel>(await result.Content.ReadAsStringAsync())
                    };
                }
                return new ResultModel<ProductModel>
                {
                    Errors = new List<string> { "Product not created!" }
                };
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException.Message);
                return new ResultModel<ProductModel>
                {
                    Errors = new List<string> { "Connection error!" }
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id}");
                if(result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }catch(HttpRequestException httpRequestException)
            {
                return false;
            }
        }

        public async Task<ResultModel<IEnumerable<ProductModel>>> GetAllAsync()
        {
            var resultModel = new ResultModel<IEnumerable<ProductModel>>();
            try
            {
                var result = await _httpClient.GetAsync($"{_httpClient.BaseAddress}");
                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    resultModel.Data = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(content);
                    return resultModel;
                }
                resultModel.Errors = new List<string> { "Something went wrong!" };
                return resultModel;
                
            }catch(HttpRequestException httpRequestException)
            {
                resultModel.Errors = new List<string>{"Connection error" };
                return resultModel;
            }
        }

        public async Task<ResultModel<ProductModel>> UpdateAsync(CreateOrUpdateProductModel newProduct)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/{newProduct.Id}", newProduct);
                if (result.IsSuccessStatusCode)
                {
                    return new ResultModel<ProductModel>
                    {
                        Data = JsonSerializer.Deserialize<ProductModel>(await result.Content.ReadAsStringAsync())
                    };
                }
                return new ResultModel<ProductModel>
                {
                    Errors = new List<string> { "Product not updated!" }
                };
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException.Message);
                return new ResultModel<ProductModel>
                {
                    Errors = new List<string> { "Connection error!" }
                };
            }
        }
    }
}
