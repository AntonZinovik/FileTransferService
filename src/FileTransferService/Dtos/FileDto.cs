namespace FileTransferService.Dtos;

/// <summary>
/// Дто отправляемого файла.
/// </summary>
/// <param name="FileName">Имя файла.</param>
/// <param name="HashCode">Хеш-сумма файла.</param>
public record FileDto(string FileName, byte[] HashCode)
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; set; } = FileName;

    /// <summary>
    /// Хеш-сумма файла.
    /// </summary>
    public byte[] HashCode { get; set; } = HashCode;
}