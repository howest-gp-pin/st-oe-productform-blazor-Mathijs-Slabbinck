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
    public class CategoryApiService : ICategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/categories");
        }

        public async Task<ResultModel<CategoryModel>> CreateAsync(CreateOrUpdateCategoryModel newCategory)
        {
            newCategory.Image = "https://placeimg.com/640/480/any";
            var result = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}",newCategory);
            if(result.IsSuccessStatusCode)
            {
                return new ResultModel<CategoryModel>
                {
                    Data = JsonSerializer.Deserialize<CategoryModel>(await result.Content.ReadAsStringAsync())
                };
            }
            return new ResultModel<CategoryModel>
            {
                Errors = new List<string> { "Category not created!" }
            };
        }

        public async Task<ResultModel<IEnumerable<CategoryModel>>> GetAllAsync()
        {
            var resultModel = new ResultModel<IEnumerable<CategoryModel>>();
            try
            {
                var result = await _httpClient.GetAsync($"{_httpClient.BaseAddress}");
                if (!result.IsSuccessStatusCode)
                {
                    resultModel.Errors = new List<string> { "Something went wrong!" };
                    return resultModel;
                }
                var content = await result.Content.ReadAsStringAsync();
                resultModel.Data = JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(content);
                return resultModel;
            }catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException.Message);
                resultModel.Errors = new List<string> { "Connection error!" };
                return resultModel;
            }
        }
        //write a delete method in the CategoryApiService
        //call the https://api.escuelajs.co/api/v1/categories/{id} endpoint
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                //call the endpoint
                var result = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id}");
                //evaluate true false
                //return true || false
                return result.IsSuccessStatusCode;
            }catch(HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException.Message);
                return false;
            }
        }

        public async Task<ResultModel<CategoryModel>> UpdateAsync(CreateOrUpdateCategoryModel newCategory)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/{newCategory.Id}",newCategory);
                if (result.IsSuccessStatusCode)
                {
                    return new ResultModel<CategoryModel>
                    {
                        Data = JsonSerializer.Deserialize<CategoryModel>(await result.Content.ReadAsStringAsync())
                    };
                }
                return new ResultModel<CategoryModel>
                {
                    Errors = new List<string> { "Category not updated!" }
                };
            }catch(HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException.Message);
                return new ResultModel<CategoryModel>
                {
                    Errors = new List<string> { "Connection error!" }
                };
            }
        }
    }
}
