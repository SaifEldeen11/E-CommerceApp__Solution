using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace E_CommerceApp.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next,ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //  Request


                await _next.Invoke(httpContext);

                // Response
                await HandleNotFoundEndPoint(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in the");

                // set Status Code
                await HandleExceptionAsync(httpContext, ex);

            }


        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            // set content type

            // create response message

            var responseMessage = new ErrorToReturn
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message,
                Errors = ex switch
                {
                    BadRequestException badRequestException => badRequestException.Errors,
                    _ => []
                }
            };

            await httpContext.Response.WriteAsJsonAsync(responseMessage);
        }

        private static async Task HandleNotFoundEndPoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var responseMessage = new ErrorToReturn
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = $"End Point {httpContext.Request.Path} is Not Found"

                };
                await httpContext.Response.WriteAsJsonAsync(responseMessage);
            }
        }
    }
}
