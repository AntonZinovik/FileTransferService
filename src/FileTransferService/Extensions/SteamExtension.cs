namespace FileTransferService.Extensions;

using System.Security.Cryptography;

/// <summary>
/// Расширение для потока данных.
/// </summary>
public static class SteamExtension
{
    /// <summary>
    /// Вычисление хэш-суммы файла.
    /// </summary>
    /// <param name="stream">Поток даннах файла.</param>
    /// <returns>Хэш-сумма.</returns>
    public static async Task<byte[]> CalculationHashCodeAsync(this Stream stream)
    {
        return await SHA512.HashDataAsync(stream);
    }
}