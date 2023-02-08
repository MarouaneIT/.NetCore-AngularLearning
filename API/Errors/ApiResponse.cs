using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int sttatusCode, string message=null)
        {
            StatusCode = sttatusCode;
            Message = message ?? GetDefaultMessageForStatusCode(sttatusCode);
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request,you have mad",
                401 => "Authorised,you are not",
                404 => "Resource found,it was not",
                500 => "Errors are the path to the darkside.Errors lead to anger.nger leads to hat," +
                "Hate leads to Carrer change",
                _ => null
            };
        }



    } 
}
