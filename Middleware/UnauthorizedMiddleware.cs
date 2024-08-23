using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new
            {
                error = "Unauthorized access",
                statusCode = 401
            });

            await context.Response.WriteAsync(result);
        }
    }
}