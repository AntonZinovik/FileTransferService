namespace FileTransferService.Options;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Настройки файлов. 
/// </summary>
public class FilesOptions
{
    /// <summary>
    /// Размер буффера для разделенной части файла.
    /// </summary>
    [Required(ErrorMessage = "Указан некорректный размер буфера для разделенной части файла")]
    public uint BufferSize { get; init; }

    /// <summary>
    /// Директория для сохранения частей файла.
    /// </summary>
    [Required(ErrorMessage = "Указана некорректная директория для сохранения частей файла")]
    public string? PathChunkDirectory { get; init; }
}