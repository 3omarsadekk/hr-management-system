namespace HRManagementSystem.WebApi.Middleware;

/// <summary>
/// Sample middleware - Replace with your actual middleware
/// </summary>
public class SampleMiddleware
{
    private readonly RequestDelegate _next;

    public SampleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Add your logic before the next middleware
        await _next(context);
        // Add your logic after the next middleware
    }
}
