using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin.Products.Core.Services.Models
{
    public class ResultModel<T>
    {
        public bool IsSuccess => !Errors.Any();
        public List<string> Errors { get; set; } = new();
        public T Data { get; set; }
    }
}
