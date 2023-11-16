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
        var hashData = await SHA512.HashDataAsync(stream);
        return hashData;
    }
}