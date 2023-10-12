namespace FPTBook.Middleware
{
    public class SimpleResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public SimpleResponseMiddleware(RequestDelegate next)
        {
        _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
        await context.Response.WriteAsync("Chao Ban, Chung toi dang tam ngung phuc vu!");
        //await _next(context);
        }
    }
}