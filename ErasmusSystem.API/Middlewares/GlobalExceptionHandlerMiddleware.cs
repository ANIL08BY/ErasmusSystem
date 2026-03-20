using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ErasmusSystem.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Her şey düzgünse sisteme devam et
                await _next(context);
            }
            catch (Exception ex)
            {
                // Bir hata olursa yakala
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sistemde beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                DetailedError = exception.Message // Sadece geliştirme aşamasında açık
            });

            return context.Response.WriteAsync(result);
        }
    }
}