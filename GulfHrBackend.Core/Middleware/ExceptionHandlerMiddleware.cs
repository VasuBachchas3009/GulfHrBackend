using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GulfHrBackend.Core.DTO;
using Microsoft.AspNetCore.Http;
namespace GulfHrBackend.Core.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            CustomExceptionDto<string> customExceptionDto = new CustomExceptionDto<string>();
            customExceptionDto.RequestId = "123456-1234-1235-4567489798";
            if (exception.Message == "Invalid User Input")
            {
                context.Response.StatusCode = 400;
                customExceptionDto.Code = 400;
                customExceptionDto.Status = "Error";
                customExceptionDto.Message = exception.Message;
                customExceptionDto.Errors = "";
                customExceptionDto.data = "";
            }
            else if(exception.Message =="The Requested Resource Was Not Found")
            {
                context.Response.StatusCode = 404;
                customExceptionDto.Code = 404;
                customExceptionDto.Status = "Failure";
                customExceptionDto.Message = exception.Message;
                customExceptionDto.Errors = "";
                customExceptionDto.data = "";
            }
            else
            {
                context.Response.StatusCode = 500;
                customExceptionDto.Code = 500;
                customExceptionDto.Status = "Error";
                customExceptionDto.Message = "There was an internal server error";
                customExceptionDto.Errors = "";
                customExceptionDto.data = "";
            }
            var result = JsonSerializer.Serialize(customExceptionDto);

            return context.Response.WriteAsync(result);
        }
    }
}
