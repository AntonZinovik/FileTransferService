namespace FileTransferService.Options;

/// <summary>
/// Настройки файлов. 
/// </summary>
public class FilesOptions
{
    /// <summary>
    /// Размер буффера для разделенной части файла.
    /// </summary>
    public uint BufferSize { get; set; }

    /// <summary>
    /// Директория для сохранения частей файла.
    /// </summary>
    public string? PathChunkDirectory { get; set; }
}