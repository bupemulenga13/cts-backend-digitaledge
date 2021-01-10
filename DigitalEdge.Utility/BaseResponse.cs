using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Utility
{
    public class BaseResponse
    {
        public BaseResponse()
        {

        }
        public BaseResponse(bool success, string message, int statusCode)
        {
            this.Success = success;
            this.Message = message;
            this.StatusCode = statusCode;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public int StatusCode  { get; set; }

    }
}
