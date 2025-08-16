using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using ZeroLine.API.Helper;

namespace ZeroLine.API.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        public ExceptionsMiddleware(RequestDelegate next, IHostEnvironment env, IMemoryCache cache)
        {
            _next = next;
            _env = env;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecuirty(context);
                if (IsRequestAllowed(context) == false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too Many Request, please try again later");
                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _env.IsDevelopment() ?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
        

        public class RateState
        {
            public DateTime WindowStart { get; set; }
            public int Count { get; set; }
        }

        public bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var cacheKey = $"Rate{ip}";
            var now = DateTime.Now;

            var state = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpiration = now.Add(_rateLimitWindow); 
                return new RateState { WindowStart = now, Count = 0 };
            });

            if (state.Count < 8)
            {
                state.Count++;
                _cache.Set(cacheKey, state, state.WindowStart.Add(_rateLimitWindow) - now);
                return true;
            }
            return false;
        }
        private void ApplySecuirty(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("Permissions-Policy", "geolocation=(), microphone=()");
        }

    }

}
