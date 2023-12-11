namespace FileTransferService.Extensions;

using System.Security.Cryptography;

/// <summary>
/// Расширение для потока данных.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Вычисление хэш-суммы файла.
    /// </summary>
    /// <param name="stream">Поток данных файла.</param>
    /// <returns>Хэш-сумма.</returns>
    public static async Task<byte[]> CalculationHashCodeAsync(this Stream stream)
    {
        stream.Position = 0;
        var hashData = await SHA1.HashDataAsync(stream);

        return hashData;
    }
}