namespace FileTransferService.Extensions;

using System.Text.Json;

using FileTransferService.Exceptions;

/// <summary>
/// Расширение для HttpResponseMessage.
/// </summary>
public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Получение ошибок из ответа.
    /// </summary>
    /// <param name="httpResponseMessage">Http ответ.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <returns>Ошибка внешней системы.</returns>
    public static async Task GetExceptionsAsync(this HttpResponseMessage httpResponseMessage
        , CancellationToken cancellationToken)
    {
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return;
        }

        var exception = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var externalSystemException = JsonSerializer.Deserialize<ExternalSystemException>(exception);

        throw externalSystemException!;
    }
}