namespace MikesEshop.Host.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ArgumentException ex)
        {
            // ArgumentException is thrown by Guard clauses
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync($"Bad Request: {ex.Message}");
        }
        catch (Exception ex)
        {
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsync($"Internal Server Error: {ex.Message}");
        }
    }
}