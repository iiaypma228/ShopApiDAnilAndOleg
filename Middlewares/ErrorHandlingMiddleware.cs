namespace Server.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            // Создаем JSON-ответ с информацией об ошибке
            var errorResponse = $"Виникла помилка\n{ex.Message}";

            // Отправляем ответ в формате JSON
            await context.Response.WriteAsync(errorResponse);
        }
    }
}