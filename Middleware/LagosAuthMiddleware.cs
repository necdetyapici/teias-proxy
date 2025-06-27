using TeiasProxy.Data;
using Microsoft.EntityFrameworkCore;

namespace TeiasProxy.Middleware
{
    public class LagosAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public LagosAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ProxyDbContext dbContext)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (path is "/openapi.json" or "/openapi.yaml" or "/openapi" or "/swagger" or "/swagger/index.html" or "/openapi/v1.json")
            {
                await _next(context);
                return;
            }

            var headers = context.Request.Headers;

            if (!headers.TryGetValue("username", out var requestUsername) ||
                !headers.TryGetValue("password", out var requestPassword))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Missing credentials.");
                return;
            }

            // Veritabanındaki şifreli kullanıcılar
            var firm = await dbContext.LagosCredentials.FirstOrDefaultAsync();

            if (firm == null ||
                firm.Username != requestUsername || firm.Password != requestPassword)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid credentials.");
                return;
            }

            await _next(context);
        }
    }
}
