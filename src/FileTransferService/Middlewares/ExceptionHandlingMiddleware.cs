namespace FileTransferService.Middlewares;

using System.Net;
using System.Text.Json;

using FileTransferService.Exceptions;

/// <summary>
///  Промежуточный слой для обработки ошибок.
/// </summary>
public class ExceptionHandlingMiddleware
{
    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Функция, обрабатывающая HTTP-запрос.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <inheritdoc cref="ExceptionHandlingMiddleware"/>
    /// <param name="next">Функция, обрабатывающая HTTP-запрос.</param>
    /// <param name="logger">Логгер.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Выполнение асинхронной операции. 
    /// </summary>
    /// <param name="httpContext">HttpContext запроса.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (FileNotFoundException exception)
        {
            await HandleExceptionAsync(httpContext,
                exception.Message,
                HttpStatusCode.NotFound,
                "Указанный файл не найден");
        }
        catch (ExternalSystemException exception)
        {
            await HandleExceptionAsync(httpContext,
                exception.Message,
                HttpStatusCode.BadRequest,
                "Ошибка внешней системы.");
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext,
                exception.Message,
                HttpStatusCode.InternalServerError,
                "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    ///  Метод обработки исключений.
    /// </summary>
    /// <param name="httpContext">HttpContext запроса.</param>
    /// <param name="exceptionMessage">Сообщение ошибки для логгера.</param>
    /// <param name="httpStatusCode">Статус код.</param>
    /// <param name="message">Сообщение ошибки отображаемое пользователю.</param>
    private async Task HandleExceptionAsync(HttpContext httpContext, string exceptionMessage,
        HttpStatusCode httpStatusCode, string message)
    {
        _logger.LogError("{ExceptionMessage}", exceptionMessage);

        var response = httpContext.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var result = JsonSerializer.Serialize(new
        {
            StatusCode = (int)httpStatusCode,
            Message = message
        });

        await response.WriteAsync(result);
    }
}