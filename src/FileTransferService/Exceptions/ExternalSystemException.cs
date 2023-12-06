namespace FileTransferService.Exceptions;

/// <summary>
/// Класс ошибки внешней системы.
/// </summary>
[Serializable]
public class ExternalSystemException : Exception
{
    /// <summary>
    /// <inheritdoc cref="ExternalSystemException"/>
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    public ExternalSystemException(string message)
        : base(message)
    {
        Message = message;
    }

    /// <summary>
    /// <inheritdoc cref="ExternalSystemException"/>
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    /// <param name="inner">Ошибка.</param>
    public ExternalSystemException(string message, Exception inner)
        : base(message, inner)
    {
        Message = message;
    }

    /// <summary>
    /// Сообщение ошибки.
    /// </summary>
    public new string Message { get; set; }
}