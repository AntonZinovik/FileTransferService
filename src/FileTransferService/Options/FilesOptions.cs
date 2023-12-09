namespace FileTransferService.Options;

/// <summary>
/// Настройки файлов. 
/// </summary>
public class FilesOptions
{
    /// <summary>
    /// Размер буффера для разделенной части файла.
    /// </summary>
    public uint BufferSize { get; init; }

    /// <summary>
    /// Директория для сохранения частей файла.
    /// </summary>
    public string? PathChunkDirectory { get; init; }
}