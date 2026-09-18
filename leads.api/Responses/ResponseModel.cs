using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leads.api.Responses
{
    public class ResponseModel<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }


        public static ResponseModel<T> WithSuccess(T data, string message)
        {
            var response = new ResponseModel<T>()
            {
                Data = data,
                Message = message
            };

            return response;
        }

        public static ResponseModel<T> WithFail(string message)
        {
            var response = new ResponseModel<T>()
            {
                Success = false,
                Message = message
            };

            return response;
        }
    }
}
