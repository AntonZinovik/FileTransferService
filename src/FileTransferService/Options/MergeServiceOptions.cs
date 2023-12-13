namespace FileTransferService.Options;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Настройки конечных точек сервиса объединения.
/// </summary>
public class MergeServiceOptions
{
    /// <summary>
    /// Адрес хоста сервиса объединения частей файлов.
    /// </summary>
    [Required(ErrorMessage = "Указан некорректный адрес хоста.")]
    public string? MergeServiceHost { get; init; }
    
    /// <summary>
    /// Конечная точка запроса объединения файла.
    /// </summary>
    [Required(ErrorMessage = "Указана некорректная точка запроса объединения файла.")]
    public string? MergeFileEndPoint { get; init; }

    /// <summary>
    /// Конечная точка запроса сохранения чанка.
    /// </summary>
    [Required(ErrorMessage = "Указана некорректная точка запроса сохранения чанка.")]
    public string? SaveChunkEndpoint { get; init; }
}