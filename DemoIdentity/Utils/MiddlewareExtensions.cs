using FPTBook.Middleware;
namespace FPTBook.Utils
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleResponseMiddleware>();
        }
    }
}
