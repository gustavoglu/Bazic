using Microsoft.AspNetCore.Builder;

namespace Bazic.Service.Api.Middlewares.ErrorException
{
    public static class ErrorExceptionMiddlewareFactory
    {
        public static IApplicationBuilder UseErrorExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorExceptionMiddleware>();
        }
    }
}
