namespace Httpclient.CookieAuth.WebApi.Middlewares
{
    public class RequestLoggerMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggerMiddleware> _logger;

        public RequestLoggerMiddleware(ILogger<RequestLoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            var start = DateTime.UtcNow;

            _logger.LogInformation("[RequestLoggerMiddleware] {method} : {path}" , method,path);

            await next(context);

            var duration = DateTime.UtcNow - start;

            _logger.LogInformation("[RequestLoggerMiddleware] {method} : {path} -- {duration} ms"
                ,method,path,duration.TotalMilliseconds);
        }
    }
}
