using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Core.Extensions
{
    public class ExceptionMiddleware
    {

        private RequestDelegate _next;

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
            catch (Exception e)
            {

                await HandleExeptionAsync(httpContext, e);
            }
        }

        private async Task HandleExeptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";

            if (e is FluentValidation.ValidationException validationException)
            {
                var errors = validationException.Errors; // IEnumerable<ValidationFailure>

                await httpContext.Response.WriteAsync(
                    new ValidationErrorDetails
                    {
                        Errors = errors,
                        Message = validationException.Message,
                        StatusCode = 400
                    }.ToString()
                );
                return;
            }

            await httpContext.Response.WriteAsync(
                new ErrorHandlerDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = e.Message
                }.ToString()
            );
        }


    }
}
