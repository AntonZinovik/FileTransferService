namespace FileTransferService.Extensions;

using System.Text.Json;

using FileTransferService.Exceptions;

/// <summary>
/// Расширение для HttpResponseMessage.
/// </summary>
public static class ResponseExtension
{
    /// <summary>
    /// Получение ошибок из ответа.
    /// </summary>
    /// <param name="httpResponseMessage">Http ответ.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <returns>ExternalSystemException.</returns>
    public static async Task<ExternalSystemException?> GetExceptions(this HttpResponseMessage httpResponseMessage
        , CancellationToken cancellationToken)
    {
        var exception = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var externalSystemException = JsonSerializer.Deserialize<ExternalSystemException>(exception);
        return externalSystemException;
    }
}