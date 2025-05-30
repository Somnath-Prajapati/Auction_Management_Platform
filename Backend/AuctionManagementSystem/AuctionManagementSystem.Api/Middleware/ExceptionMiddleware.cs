using System.Net;
using AuctionManagementSystem.Application.Exceptions;

namespace AuctionManagementSystem.Api.Middleware
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
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

        private static async Task<Task> HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case DatabaseException dbException:
                    statusCode = HttpStatusCode.InternalServerError; 
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            httpContext.Response.StatusCode = (int)statusCode;
            var response = new
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            };
            return httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}