using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleProject.Core.Models
{
    public class ResponseModel<T>
    {
        public string ResponseMessage { get; set; }
        public ApiResponseCodes ResponseCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasError => this.Errors.Any();
        public T Data { get; set; }

        public ResponseModel()
        {

        }

        public ResponseModel(T data, string responseMessage, ApiResponseCodes apiResponseCode, List<string> errors)
        {
            this.Data = data;
            this.ResponseCode = apiResponseCode;
            this.Errors = errors;
            this.ResponseMessage = responseMessage;
        }

        public static ResponseModel<T> Succeeded(T data, string responseMessage = "Success", ApiResponseCodes apiResponseCode = ApiResponseCodes.OK)
        {
            var response = new ResponseModel<T>
            {
                Data = data,
                ResponseCode = apiResponseCode,
                ResponseMessage = responseMessage
            };
            return response;
        }

        public static ResponseModel<T> Failed(string responseMessage, ApiResponseCodes apiResponseCode, List<string> errors)
        {
            var response = new ResponseModel<T>
            {
                Errors = errors,
                ResponseCode = apiResponseCode,
                ResponseMessage = responseMessage,                
            };
            return response;
        }
    }

    public enum ApiResponseCodes
    {
        OK = 200, BAD_REQUEST = 400, INTERNAL_SERVER_ERROR = 500, NOT_FOUND = 404,

    }
}
