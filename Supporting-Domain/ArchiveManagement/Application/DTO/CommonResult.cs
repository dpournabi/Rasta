using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{    
    public class CommonResult<T>
    {
        internal CommonResult(bool succeeded, string? message, IEnumerable<string>? errors, T? data)
        {
            Succeeded = succeeded;
            Errors = errors?.ToArray();
            Message = message;
            Data = data;
        }
        public T? Data { get; set; }
        public bool Succeeded { get; init; }
        public string? Message { get; init; }

        public string[]? Errors { get; init; }

        public static CommonResult<T> Success(string message, T? data)
        {
            return new CommonResult<T>(true, message, Array.Empty<string>(), data);
        }

        public static CommonResult<T> Failure(string error, IEnumerable<string>? errors, T? data)
        {
            return new CommonResult<T>(false, error, errors, data);
        }
    }
}
