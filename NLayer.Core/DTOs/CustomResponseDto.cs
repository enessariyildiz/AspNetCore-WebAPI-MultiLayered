using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }

        public static CustomResponseDto<T> Succes(int statcusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statcusCode };
        }
        public static CustomResponseDto<T> Succes(int statcusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statcusCode };
        }
        public static CustomResponseDto<T> Fail(int statcusCode, List<String> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statcusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statcusCode, string error)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statcusCode,
                Errors = new List<string> { error }
            };  
        }
    }
}
