namespace Pin.Products.Core.Services.Models
{
    public abstract class BaseResult
    {
        public bool IsSuccess => !Errors.Any();
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ResultModel<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
