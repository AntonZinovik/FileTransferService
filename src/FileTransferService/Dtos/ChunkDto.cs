namespace FileTransferService.Dtos;

/// <summary>
/// Дто части файла.
/// </summary>
/// <param name="FileName">Имя файла.</param>
/// <param name="Bytes">Отправляемая часть файл.</param>
/// <param name="HashCode">Хеш-сумма файла.</param>
/// <param name="ChunkName">Имя части файла.</param>
public record ChunkDto(string FileName, byte[] Bytes, byte[] HashCode, string ChunkName)
{
    /// <summary>
    /// Отправляемый файл.
    /// </summary>
    public byte[] Bytes { get; } = Bytes;

    /// <summary>
    /// Имя чанка.
    /// </summary>
    public string ChunkName { get; } = ChunkName;

    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; } = FileName;

    /// <summary>
    /// Хеш-сумма файла.
    /// </summary>
    public byte[] HashCode { get; } = HashCode;
}