namespace FileTransferService.Exceptions;

/// <summary>
/// Класс ошибки при сравнении хэш-сумм.
/// </summary>
[Serializable]
public class HashCodeException : Exception
{
    /// <inheritdoc cref="HashCodeException"/>
    public HashCodeException()
    {
    }

    /// <summary>
    /// <inheritdoc cref="HashCodeException"/>
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    public HashCodeException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// <inheritdoc cref="HashCodeException"/>
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    /// <param name="inner">Ошибка.</param>
    public HashCodeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}