namespace FileTransferService.Options;

/// <summary>
/// Настройки файлов. 
/// </summary>
public class FilesOptions
{
    /// <inheritdoc cref="FilesOptions"/>
    /// <param name="bufferSize">Размер буффера для разделенной части файла.</param>
    /// <param name="pathChunkDirectory">Директория для сохранения частей файла.</param>
    public FilesOptions(int bufferSize, string pathChunkDirectory)
    {
        PathChunkDirectory = pathChunkDirectory;
        BufferSize = bufferSize;
    }

    /// <summary>
    /// Размер буффера для разделенной части файла.
    /// </summary>
    public int BufferSize { get; set; }

    /// <summary>
    /// Директория для сохранения частей файла.
    /// </summary>
    public string PathChunkDirectory { get; set; }
}